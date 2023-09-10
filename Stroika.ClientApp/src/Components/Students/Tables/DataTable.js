import React, { useState }  from 'react'
import { Table, Button } from 'reactstrap';
import ModalForm from '../Modals/ModalForm'
import Moment from 'moment';
import   {StudentsService}  from "../StudentsService"
import store from '../../../reducer'
function DataTable(props){
  const [role, setRole] = useState(store.getState().role);
  const deleteItem = id => {
    let confirmDelete = window.confirm('Delete item forever?')
    if(confirmDelete){
      StudentsService.DeleteStudent(id)
      .then(item => {
        props.deleteItemFromState(id)
      })
      .catch(err => console.log(err))
    }
  }
  store.subscribe(() => {
    setRole(store.getState().role);
    console.log(store.getState());
  });
  const items = props.items.map(item => {
    return (
      <tr key={item.id}>
        <th scope="row">{item.id}</th>
        <td>{item.firstName}</td>
        <td>{item.lastName}</td>
        <td>{ Moment(item.dateOfBirth).format('yyyy-MM-DD') }</td> 
        <td>
          <div style={{width:"110px"}}>
            <ModalForm buttonLabel={role=="Admin"?"Edit":"View"} nationalities={props.nationalities} id={item.id} updateState={props.updateState}/>
            {' '}
         {role=="Admin"? <Button color="danger" onClick={() => deleteItem(item.id)}>Del</Button>:''}  
          </div>
        </td>
      </tr>
      )
    })

  return (
    <Table responsive hover>
      <thead>
        <tr>
          <th>ID</th>
          <th>firstName</th>
          <th>lastName</th>
          <th>dateOfBirth</th>
           
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        {items}
      </tbody>
    </Table>
  )
}

export default DataTable