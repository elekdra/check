import React, { useRef, useState } from 'react';
import './AppLogin.css';

import { useHistory } from 'react-router';
import UserAuthentication from '../../ApiServices/UserAuthentication'
function AppLogin({ setUsername }) {
  const history = useHistory();
  const nameForm = useRef(null);
  const [errorMessage, setErrorMessage] = useState('');
  const handleClickEvent = (props) => {
    const form = nameForm.current;
    const userName = form['username'].value;
    const password = form['password'].value;
    let validUser = UserAuthentication(userName, password).then((value) => {
      console.log(value);
      if (value.data) {
        setUsername(userName);
        history.push('/trainingdocuments');
        localStorage.setItem('auth', 'true');
      } else {
        setErrorMessage('Incorrect username or password');
      }
    });
  };
  return (
    <div id='loginform'>
      <h2 id='headerTitle'>Login</h2>
      <div className='form'>
        <form ref={nameForm}>
          <p className='errormsg' id='errormsg'>
            {errorMessage}
          </p>
          <div className='row'>
            <label>User ID</label>
            <input type='text' placeholder='User ID' name={'username'} />
          </div>
          <div className='row'>
            <label>Password</label>
            <input name={'password'} type='password' placeholder='Password' />
          </div>
        </form>
        <div id='button' className='row'>
          <button className='submitbutton' onClick={handleClickEvent}>
            Login
          </button>
        </div>
      </div>
    </div>
  );
}

export default AppLogin;
