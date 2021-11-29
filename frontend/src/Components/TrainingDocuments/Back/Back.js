import React from 'react';
import { useHistory } from 'react-router-dom';
import './Back.css';
import arrow from '../../../assets/arrow.png';
function Back(props) {
  let history = useHistory();
  function BacktoAdminPage() {
    if (props.page === '/trainingdocuments') {
      history.push('/trainingdocuments');
    } else {
      history.push('/dashboard');
    }
  }
  return (
    <div>
      <button className='back-button' onClick={BacktoAdminPage}>
        <img src={arrow} alt='' />
        <span>Back</span>
      </button>
    </div>
  );
}

export default Back;
