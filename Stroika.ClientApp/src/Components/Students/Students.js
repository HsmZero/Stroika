import React, { useState, useEffect ,useRef} from "react";
import { Container, Row, Col } from "reactstrap";
 import DataTable from "./Tables/DataTable"
 import ModalForm from "./Modals/ModalForm"
 import   {StudentsService}  from "./StudentsService"
 import store from '../../reducer'
 
 
function Students (props) { 
  const [nationalities, setNationalities] = useState([]);
  const [role, setRole] = useState(store.getState().role );
  const [allStudents, setAllStudents] = useState([
    // {
    //   id: 0,
    //   firstName: "Hytham",
    //   lastName: "Ali",
    //   dateOfBirth:  new Date(1987,10,15),
    //   nationalityId:0
    // }
  ]);
  store.subscribe(() => { 
    setRole(store.getState().role);
    console.log(store.getState());
    
  });
 
  
 
  const GetAllStudents = () => {
    StudentsService. GetAllStudents() 
      .then((response) => { 

        setAllStudents(response.data);
      })
      .catch((err) => console.log(err));
  };
  const GetAllNationalities = () => {
    StudentsService. GetAllNationalities() 
      .then((response) => setNationalities(response.data))
      .catch((err) => console.log(err));
  };
  const addNewStudent = (student) => {
    debugger; 
     setAllStudents([...allStudents, student]) 
  };

  const updateState = (item) => {
    const itemIndex = allStudents.findIndex((data) => data.id === item.id);
    const newArray = [
      ...allStudents.slice(0, itemIndex),
      item,
      ...allStudents.slice(itemIndex + 1)
    ];
    setAllStudents(newArray); 
  };

  const deleteStudent= (id) => {
    const newArray = allStudents.filter((item) => item.id !== id);
      setAllStudents(newArray);
   
  };

  useEffect(() => {
    GetAllStudents();
    GetAllNationalities();

  }, []);
  const onChange = (e) => {
  
    store.dispatch({
      type:  e.target.value
    });
   
  };
  return (
    <Container >
         <Row>
        <Col>
          <h5 style={{ margin: "20px 0" }}>Roles: <select value={role}  onChange={onChange} >
            <option>Register</option>
            <option>Admin</option>
            </select></h5>
        </Col>
      </Row>
      <Row>
        <Col>
          <h1 style={{ margin: "20px 0" }}>Students</h1>
        </Col>
      </Row>
      <Row>
        <Col> {
          role=="Admin"?'':      <ModalForm   nationalities={nationalities}   buttonLabel="Add Student" addItemToState={addNewStudent} />
    
        }
        </Col>
      </Row>
      <Row>
        <Col>
          <DataTable
            items={allStudents}
            updateState={updateState}
            deleteItemFromState={deleteStudent}
            nationalities={nationalities}
          />
        </Col>
      </Row>
    
    </Container>
  );
}

export default Students ;
