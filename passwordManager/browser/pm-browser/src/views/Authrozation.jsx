import React, { useState, useEffect } from "react";
import { toast } from "react-toastify";
import { sha256 } from "js-sha256";

function Authrozation({ onLogin }) {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [signUpName, setSignUpName] = useState("");
    const [signUpPassword, setSignUpPassword] = useState("");  

    const baseUrl = "https://localhost:8081/api/";

    const handleLogin = (event) => {
        event.preventDefault();
        fetch(baseUrl + "User/getSalt?username=" + username)
        .then((response) => {
            if (!response.ok) {
                toast.error("Login failed")
                console.log(response);
            } else {
                toast.success("Login successful")
            }
            return response.json()})
        .then(data => {
            if (!data.salt) {
                toast.error("User not found");
                return;
            } else {
                var salt = data.salt;
                var hashedPassword = sha256(password + salt);
                fetch(baseUrl + "User/login", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({ 
                        username: username, 
                        masterPasswordHashed: hashedPassword })
                }).then(response => response.json())
                .then(data => {
                    if (data.accessToken) {
                        const accessToken = data.accessToken;
                        const derivedKeySalt = data.derivedKeySalt;
                        onLogin(username, hashedPassword, accessToken, derivedKeySalt);
                    } else {
                        alert("Invalid password");
                    }
                });
            }}
        );
    }

    const handleSignUp = (event) => {
        event.preventDefault();
        const salt = generateSalt();
        const deviredKeySalt = generateSalt();
        console.log(salt, "salt");
        console.log(deviredKeySalt, "deviredKeySalt");
        console.log(signUpName, "signUpName");
        console.log(signUpPassword, "signUpPassword")
        fetch(baseUrl + "User/register", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({ 
                username: signUpName, 
                masterPasswordHashed: sha256(signUpPassword + salt),
                salt: salt,
                derivedKeySalt: deviredKeySalt
             })
        }).then((response) => {
            if (!response.ok) {
                toast.error("Sign up failed");
            }
            else {
                toast.success("Sign up successful");
            }
            return response.json()})
    }

    const generateSalt = () => {
        const length = 32;
        const array = new Uint8Array(length);
        window.crypto.getRandomValues(array);
        const password = Array.from(array, byte => String.fromCharCode(byte)).join('');
        return btoa(password).slice(0, length);
    }

    return (
        <div>
            <h1>COMP.SEC.300</h1>
            <div>
                <h2>Login</h2>
                <form>
                    <label>Username</label><br />
                    <input type="text" value={username} onChange={(e) => setUsername(e.target.value)} /><br />
                    <label>Password</label><br />
                    <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} /><br />
                    <button onClick={handleLogin}>Login</button>
                </form>
            </div>
            <div>
                <h2>Sign up</h2>
                <form>
                    <label>Username</label><br />
                    <input type="text" value={signUpName} onChange={(e) => setSignUpName(e.target.value)} /><br />
                    <label>Password</label><br />
                    <input type="password" value={signUpPassword} onChange={(e) => setSignUpPassword(e.target.value)} /><br />
                    <button onClick={handleSignUp}>Sign up</button>
                </form>
            </div>
        </div>
    );
}

export default Authrozation;