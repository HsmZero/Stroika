
import React, { Component } from 'react';
 
 
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
 
import  Students  from './Components/Students/Students';
import  StudentFamily  from './Components/StudentFamily/StudentFamily';
class App extends Component {
  render() {
    return (  
 
        <Router>
          <Switch>
            <Route path='/' exact={true} component={Students }/>
            <Route path='/Students ' exact={true} component={Students }/>
            <Route path='//Students/:id/Family ' exact={true} component={StudentFamily}/>
 
          </Switch>
        </Router>
       
    )
  }
}

export default App;