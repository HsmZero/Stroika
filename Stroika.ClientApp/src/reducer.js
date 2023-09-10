import {createStore} from 'redux';

import { combineReducers } from 'redux'
const AdminAction = () => ({ type: "Admin" });
const RegisterAction = () => ({ type: "Register" });

/**
 * A reducer takes two arguments, the current state and an action.
 */
  const RolesApp = (state = {role:"Register",closeEditModel:false}, action) => {
    state.closeEditModel=null;
  switch (action.type) {
    case 'Admin': 
    state.role="Admin"
      return state ;
      case 'Register': 
      state.role="Register"
      return state ; 
      case 'closeEditModel': 
      state.closeEditModel=true
      return state ; 
    default:
      return state;
  }
};

// const RolesApp = combineReducers({
//   reducer
// })



export default     createStore(RolesApp)
 