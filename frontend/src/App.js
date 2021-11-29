import React, { useState, useEffect } from 'react';
import { Route, Switch } from 'react-router-dom';
import AdminDashboard from './Components/AdminDashboard/AdminDashboard';
import AppLogin from './Components/Login/AppLogin';
import TrainingDocuments from './Components/TrainingDocuments/TrainingDocuments';
import UploadFile from './Components/UploadFile/UploadFile';
import NotImplemented from './Components/NotImplemented/NotImplemented';
import history from './Routing/History';

// all Main Components routes here

const App = () => {
  const [authState, setAuthState] = useState(false);
  const [username, setUsername] = useState('');
  const [filterParameters,setFilterParameters]=useState([]);
  const [training, setTraining] = useState('');
  const [version, setVersion] = useState('');
  const [company, setCompany] = useState('');  

  useEffect(() => {
    handleAuth();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [authState]);

  const handleAuth = () => {
    let auth = localStorage.getItem('auth');
    if (auth) {
      setAuthState(true);
    } else {
      setAuthState(false);
      history.push('/');
    }
  };

  return (
    <div>
      <Switch>
        <Route
          exact
          path='/'
          render={(props) => (
            <AppLogin
              {...props}
              username={username}
              setUsername={setUsername}
            />
          )}
        />
        <Route
          path='/trainingdocuments'
          component={() => <AdminDashboard username={username} />}
        />
        <Route path='/notfound' component={() => <NotImplemented />} />
        <Route
          exact
          path='/dashboard'
          component={() => (
            <TrainingDocuments
              training={training}
              setTraining={setTraining}
              version={version}
              setVersion={setVersion}
              company={company}
              setCompany={setCompany}
              filterParameters={filterParameters}
              setFilterParameters={setFilterParameters}
            />
          )}
        />
        <Route
          exact
          path='/uploaddocument'
          component={() => (
            <UploadFile
              training={training}
              setTraining={setTraining}
              version={version}
              setVersion={setVersion}
              company={company}
              setCompany={setCompany}
              filterParameters={filterParameters}
              setFilterParameters={setFilterParameters}
            />
          )}
        />
      </Switch>
    </div>
  );
};

export default App;
