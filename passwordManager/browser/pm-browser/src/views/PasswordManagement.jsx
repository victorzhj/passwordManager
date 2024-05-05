import React, { useState, useEffect } from "react";
import { toast } from "react-toastify";
import { sha256 } from "js-sha256";
import 'react-toastify/dist/ReactToastify.css';
import { ToastContainer } from "react-toastify";

function PasswordManagement({username, masterPassword, accessToken, derivedKeySalt, onLogout}) { 
    const [derivedKey, setDerivedKey] = useState('');
    const [passwords, setPasswords] = useState([]);
    const [decryptedPasswords, setDecryptedPasswords] = useState('');

    const baseUrl = "https://localhost:8081/api/";

    // Fetch the derived key when the master password or derived key salt changes
    useEffect(() => {
        const getDerivedKey = async () => {
            const key = await deriveKey(masterPassword, derivedKeySalt);
            setDerivedKey(key);
        }
    
        getDerivedKey();
    }, [masterPassword, derivedKeySalt]);

    // Fetch passwords when the component mounts
    useEffect(() => {
        getPasswords();
    }, []);

    // Decrypt passwords when the passwords state changes
    useEffect(() => {
        const decryptPasswords = async () => {
            const decrypted = await Promise.all(passwords.map(password => decryptPassword(password.encryptedPassword, password.iv, derivedKey)));
            setDecryptedPasswords(decrypted);
        };

        if (passwords.length > 0) {
            decryptPasswords();
        }
    }, [passwords]);

    const addPassword = async (password, site) => {
        const {encryptedPassword, iv} = await encryptPassword(password, derivedKey);
        fetch(baseUrl + "Password", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + accessToken
            }, 
            body: JSON.stringify({
                userId: 0,
                encryptedPassword: encryptedPassword,
                site: site,
                iv: iv
            })
        }).then((response) => {
            if (!response.ok) {
                toast.error("Failed to add password");
            } else {
                toast.success("Password added");
                getPasswords();
            }
        })
    }

    const getPasswords = () => {
        fetch(baseUrl + "Password", {
            headers: {
                "Authorization": "Bearer " + accessToken
            }
        }).then((response) => {
                if (!response.ok) {
                    toast.error("Failed to get passwords");
                }
                return response.json()})
        .then(data => {
            setPasswords(data)
        });
    }
    
    const deletePassword = (id) => {
        fetch(baseUrl + "Password?passwordId=" + id, {
            method: "DELETE",
            headers: {
                "Authorization": "Bearer " + accessToken
            }
        }).then((response) => {
            if (!response.ok) {
                toast.error("Failed to delete password");
            }
            else 
            {
                toast.success("Password deleted");
                getPasswords();
            }
            
        });
    }

    const deleteUser = () => {
        fetch(baseUrl + "User", {
            method: "DELETE",
            headers: {
                "Authorization": "Bearer " + accessToken
            }
        }).then((response) => {
            if (!response.ok) {
                toast.error("Failed to delete user");
            } else {
                toast.success("User deleted");
                onLogout();
            }
        });
    }
    
    // Derive a key from a password and a salt
    async function deriveKey(password, salt) {
        const enc = new TextEncoder();
        const passwordBuffer = enc.encode(password);
        const saltBuffer = enc.encode(salt);
        // Import the password as a CryptoKey for use with the Web Crypto API
        const importedKey = await window.crypto.subtle.importKey(
            'raw',
            passwordBuffer,
            {name: 'PBKDF2'},
            false,
            ['deriveKey']
        );
        // Derive a key from the imported key
        const derivedKey = await window.crypto.subtle.deriveKey(
            {
                name: 'PBKDF2',
                salt: saltBuffer,
                iterations: 1000,
                hash: 'SHA-256'
            },
            importedKey,
            { name: 'AES-GCM', length: 256 },
            true,
            ['encrypt', 'decrypt']
        );
        return derivedKey;
    }

    // Encrypt a password using AES-GCM
    const encryptPassword = async (password, key) => {
        // Generate a random IV
        const iv = window.crypto.getRandomValues(new Uint8Array(12));
        const encryptedPassword = await window.crypto.subtle.encrypt(
            {
                name: 'AES-GCM',
                iv: iv
            },
            key,
            new TextEncoder().encode(password)
        );
        // Convert the encrypted password and IV to base64 strings
        const encryptedPasswordBase64 = btoa(String.fromCharCode(...new Uint8Array(encryptedPassword)));
        const ivBase64 = btoa(String.fromCharCode(...iv));
        return {encryptedPassword: encryptedPasswordBase64, iv: ivBase64};
    }

    // Decrypt a password using AES-GCM
    const decryptPassword = async (encryptedPasswordBase64, ivBase64, key) => {
        // Convert the base64 strings to Uint8Arrays
        const encryptedPassword = Uint8Array.from(atob(encryptedPasswordBase64), c => c.charCodeAt(0)).buffer;
        const iv = Uint8Array.from(atob(ivBase64), c => c.charCodeAt(0));
        var decryptedPassword;
        try {
                 decryptedPassword = await window.crypto.subtle.decrypt(
                {
                    name: 'AES-GCM',
                    iv: iv
                },
                key,
                encryptedPassword
            );
        } catch (error) {
            console.error('Failed to decrypt password:', error);
        }
        return new TextDecoder().decode(decryptedPassword);
    }
    
    const generatePassword = (length) => {
        const array = new Uint8Array(length);
        // Generate a random password
        window.crypto.getRandomValues(array);
        // Convert the password to a base64 string
        const password = Array.from(array, byte => String.fromCharCode(byte)).join('');
        return btoa(password).slice(0, length);
    }

    return (
        <div>
            <ToastContainer />
            <h1>Password Management</h1>
            <button onClick={onLogout}>Logout</button>
            <div> 
                <h2>Delete user</h2>
                <button onClick={deleteUser}>Delete User</button>
            </div>
            <div>
                <h2>Add Password</h2>
                <input type="text" id="site" placeholder="Site" />
                <input type="text" id="password" placeholder="Password" />
                <input type="number" id="length" placeholder="Password length"/>
                <button onClick={() => {
                    const length = parseInt(document.getElementById("length").value, 10);
                    if (typeof length !== "number") 
                    {
                        toast.error("Give password length");
                    } else {
                        document.getElementById("password").value = generatePassword(length);
                    }
                }
                }>Generate Password</button>
                
                <button onClick={() => addPassword(document.getElementById("password").value, document.getElementById("site").value)}>Add Password</button>
            </div>
            <div>
                <h2>Passwords</h2>
                {passwords.map((password, index) => (
                    <div key={password.passwordId}>
                        <p>Site: {password.site}</p>
                        <p>Password: {decryptedPasswords[index]}</p>
                        <button onClick={() => deletePassword(password.passwordId)}>Delete</button>
                    </div>
                ))}
            </div>   
        </div>
    );
}

export default PasswordManagement;