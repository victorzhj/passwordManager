import './App.css';
import { useState, useEffect } from 'react';
import Authorization from './views/Authrozation';

function App() {
  return (
    <div className="App">
      <div className="Password manager">
        <h1>Password manager</h1>
          <Authorization />
        </div>
    </div>
  );
}

export default App;
