import React from 'react';

import Plan from './Plan';
import Api from './Api';

import 'primereact/resources/primereact.min.css';
import 'primeflex/primeflex.css';
import 'primeicons/primeicons.css';
import 'primereact/resources/themes/bootstrap4-light-blue/theme.css';
import './App.css';

import NavBar from './Components/NavBar';

import { BrowserRouter, Switch, Route, useHistory, useLocation } from 'react-router-dom';

const App = () => {

  return (<>
    <div className="cls" />
    <div className="App">
      <BrowserRouter>
        <NavBar />
        <div className="layout-main">
          <Switch>
            <Route path='/' component={Plan} />
          </Switch>
        </div>
      </BrowserRouter>
    </div>
  </>
  );
}

export default App;
