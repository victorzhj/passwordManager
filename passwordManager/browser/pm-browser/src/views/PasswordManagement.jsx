import React, { useState, useEffect } from "react";
import { toast } from "react-toastify";
import { sha256 } from "js-sha256";

function PasswordManagement({username, masterPassword, accessToken, derivedKeySalt, onLogout}) { 
    const [derivedKey, setDerivedKey] = useState('');
    const [passwords, setPasswords] = useState([]);
    const [decryptedPasswords, setDecryptedPasswords] = useState('');

    const baseUrl = "https://localhost:8081/api/";

    useEffect(() => {
        const getDerivedKey = async () => {
            const key = await deriveKey(masterPassword, derivedKeySalt);
            setDerivedKey(key);
        }
    
        getDerivedKey(); // Call the function
    }, [masterPassword, derivedKeySalt]); // Add dependencies

    useEffect(() => {
        getPasswords();
    }, []);

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
        //const derivedKey = await deriveKey(masterPassword, derivedKeySalt);
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
        console.log(baseUrl + "Password/" + id, "delete");
        fetch(baseUrl + "Password?passwordId=" + id, {
            method: "DELETE",
            headers: {
                "Authorization": "Bearer " + accessToken
            }
        }).then((response) => {
            if (!response.ok) {
                console.log(accessToken)
                console.log(response);
                toast.error("Failed to delete password");
            }
            else 
            {
                toast.success("Password deleted");
                getPasswords();
            }
            
        });
    }
        
    async function deriveKey(password, salt) {
        const enc = new TextEncoder();
        const passwordBuffer = enc.encode(password);
        const saltBuffer = enc.encode(salt);
        const importedKey = await window.crypto.subtle.importKey(
            'raw',
            passwordBuffer,
            {name: 'PBKDF2'},
            false,
            ['deriveKey']
        );
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

    const encryptPassword = async (password, key) => {
        const iv = window.crypto.getRandomValues(new Uint8Array(12));
        const encryptedPassword = await window.crypto.subtle.encrypt(
            {
                name: 'AES-GCM',
                iv: iv
            },
            key,
            new TextEncoder().encode(password)
        );
        const encryptedPasswordBase64 = btoa(String.fromCharCode(...new Uint8Array(encryptedPassword)));
        const ivBase64 = btoa(String.fromCharCode(...iv));
        return {encryptedPassword: encryptedPasswordBase64, iv: ivBase64};
    }

    const decryptPassword = async (encryptedPasswordBase64, ivBase64, key) => {
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

    return (
        <div>
            <h1>Password Management</h1>
            <button onClick={onLogout}>Logout</button>
            <div>
                <h2>Add Password</h2>
                <input type="text" id="site" placeholder="Site" />
                <input type="password" id="password" placeholder="Password" />
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