import React from 'react';
import './AdminAvatar.css';

import user from '../../../assets/user.png';
import Dropdown from 'react-dropdown';
import 'react-dropdown/style.css';
import { useHistory } from 'react-router-dom';

function AdminAvatar(props) {
  const options = ['Logout'];
  const defaultOption = options[0];
  const history = useHistory();

  const handleLogout = () => {
    localStorage.removeItem('auth');
    history.push('/');
  };
  return (
    <div className='navbar'>
      {/* <img className='logo' src={flag} alt=' logo' /> */}

      <div className='navbar-avatar'>
        <div style={{ paddingTop: '1em' }}>
          <label className='avatar-user'>{props.username}</label>
        </div>
        <div style={{ paddingTop: '0.4em' }}>
          <img className='avatar' src={user} alt='avatar' />
        </div>
        <div
          className='dropdropdown-content'
          style={{ marginTop: '1em', marginLeft: '0em' }}
        >
          <input
            className='logout'
            onClick={handleLogout}
            type='button'
            value=' &#9660;'
            title='logout'
          />
        </div>
      </div>
    </div>
  );
}

export default AdminAvatar;
