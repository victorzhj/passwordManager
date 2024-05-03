import './App.css';
import { useState, useEffect } from 'react';
import Authorization from './views/Authrozation';
import PasswordManagement from './views/PasswordManagement';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function App() {
  const [username, setUsername] = useState("");
  const [masterPassword, setMasterPassword] = useState("");
  const [accessToken, setAccessToken] = useState("");
  const [derivedKeySalt, setDerivedKeySalt] = useState("");
  const [loggedIn, setLoggedIn] = useState(false);

  const onLogin = (username, masterPassword, accessToken, derivedKeySalt) => {
    setUsername(username);
    setMasterPassword(masterPassword);
    setAccessToken(accessToken);
    setDerivedKeySalt(derivedKeySalt);
    setLoggedIn(true);
  }

  const onLogout = () => {
    setUsername("");
    setMasterPassword("");
    setAccessToken("");
    setDerivedKeySalt("");
    setLoggedIn(false);
  }

  return (
    <div className="App">
      <div className="Password manager">
        <h1>Password manager</h1>
        {!loggedIn && <Authorization onLogin={onLogin}/>}
        {loggedIn && <PasswordManagement onLogout={onLogout}
                                         username={username} 
                                         masterPassword={masterPassword} 
                                         accessToken={accessToken} 
                                         derivedKeySalt={derivedKeySalt} />}  
        </div>
    </div>
  );
}

export default App;
