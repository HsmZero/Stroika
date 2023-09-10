import React, { useState, useEffect } from "react";
import { Button, Form, FormGroup, Label, Input, Table } from "reactstrap";
import Moment from 'moment';
import { StudentsService } from "../../StudentsService"
import store from '../../../../reducer'
function AddEditForm(props) {
  const [student, setStudent] = useState({
    id: 0,
    firstName: "",
    lastName: "",
    dateOfBirth: new Date(),
    nationalityId: null

  });
  const [famliyMember, setFamliyMember] = useState(null);
  const [famliyMembers, setFamliyMembers] = useState([]);
  const [showTabs, setShowTabs] = useState(true)
  const [role, setRole] = useState(store.getState().role);
  const [showSaveButton, setShowSaveButton] = useState(true);
  store.subscribe(() => {
    setRole(store.getState().role);
  
  });
 
  const onChange = (e) => {
    setStudent({
      ...student,
      [e.target.name]: e.target.value
    });
  };
  const onChangeFM = (e) => {

    setFamliyMember({
      ...famliyMember,
      [e.target.name.replace("fm_", "")]: e.target.value
    });
  };

  const submitFormEdit = (e) => {
    e.preventDefault();
    student.id = props.id;
    student.famliyMembers = famliyMembers;
    console.log(props.student);
    setShowSaveButton(false);
    StudentsService.UpdateStudentWithFamliy(student).then(res => {
      debugger
      props.updateState(res.data);
      store.dispatch({
        type:  "closeEditModel"
      });
    })

  };

  const submitFormAdd = (e) => {

    e.preventDefault();
    debugger
    student.famliyMembers = famliyMembers;
    console.log(props.student);
    debugger
    setShowSaveButton(false);
    StudentsService.AddNewStudentWithFamliy(student).then(res => {
      debugger
      props.addItemToState(res.data)
      store.dispatch({
        type:  "closeEditModel"
      });
    })

  };
  const deleteFM = index => {
    debugger
    const confirmDelete = window.confirm('Delete item forever?')
    if (confirmDelete) {
      famliyMembers.splice(index, 1)
      setFamliyMembers([...famliyMembers]);
    }
  }

  const updateFM = fm => {
    debugger
    fm.index = famliyMembers.indexOf(fm);
    setFamliyMember(fm);

  }
  const addnewFM = (e) => {
    debugger
    if (!e.target.form.checkValidity())
      return;
    if (famliyMember.id == -1) {
      famliyMember.id = 0;
      famliyMembers.push(famliyMember);

    }
    else {
      const index = famliyMember.index;

      famliyMembers.splice(index, 1, famliyMember);
    }

    const newArr = [...famliyMembers];
    setFamliyMember(null)
    setFamliyMembers(newArr);

  }
const canEdit=()=>(props.id && role=="Admin")||(!props.id )

  const relationshipTypes = [{ name: "Please select Relationship", id: "" },
  { name: "Father", id: 1 },
  { name: "Mother", id: 2 },
  { name: "Sibling", id: 3 },
  ];
  const getRelationship = (id) => relationshipTypes.map(i => i.id.toString() == id?.toString() ? <option value={i.id} selected>{i.name}  </option> : <option value={i.id} >{i.name}  </option>);

  useEffect(() => {
    debugger;
    if (props.id) {
      StudentsService.GetStudentFullDataById(props.id).then(res => {
        debugger;
        let _student = res.data;
        const famliyMembers = _student.famliyMembers
        delete _student.famliyMembers 
        setStudent(_student);
        setFamliyMembers(famliyMembers);
      })

    }
  }, [props.id]);

  const getnationalitiesSelect = (id) => {
    debugger
    if (props.nationalities)
      return [<option value="">Please select</option>, ...props.nationalities.map(i => i.nationalityId.toString() == id?.toString() ? (
        <option value={i.nationalityId} selected>{i.nationalityName}  </option>
      ) : (<option value={i.nationalityId}>{i.nationalityName}</option>))];
    else
      return [<option value="" >Please select</option>];
  }
  const getSaveButton=()=>{
     
      return canEdit()&&showSaveButton ?<Button >Submit</Button>:''; 
    
     
  }
  const getFMForm = () => {
    if (famliyMember)
      return <div id="AddFamliyMembers">
        <Form>
          <FormGroup>
            <Label for="fm_firstName">First Name</Label>
            <Input required
              type="text"
              name="fm_firstName"
              id="fm_firstName"
              onChange={onChangeFM}
              value={famliyMember.firstName === null ? "" : famliyMember.firstName}
            />
          </FormGroup>
          <FormGroup>
            <Label for="fm_lastName">Last Name</Label>
            <Input
              req
              type="text"
              required
              name="fm_lastName"
              id="fm_lastName"
              onChange={onChangeFM}
              value={famliyMember.lastName === null ? "" : famliyMember.lastName}
            />
          </FormGroup>
          <FormGroup>
            <Label for="fm_dateOfBirth">Date Of Birth</Label>
            <Input required
              type="Date"
              name="fm_dateOfBirth"
              id="fm_dateOfBirth"
              onChange={onChangeFM}
              value={famliyMember.dateOfBirth === null ? new Date() : Moment(famliyMember.dateOfBirth).format('yyyy-MM-DD')}
            />
          </FormGroup>
          <FormGroup>
            <Label for="fm_nationalityId">nationality</Label>
            <select required className="form-control" id='fm_nationalityId' name='fm_nationalityId' onChange={onChangeFM}>

              {getnationalitiesSelect(famliyMember.nationalityId)}
            </select>
          </FormGroup>
          <FormGroup>
            <Label for="fm_relationshipId">nationality</Label>
            <select required className="form-control" id='fm_relationshipId' name='fm_relationshipId' onChange={onChangeFM}>
              {getRelationship(famliyMember.relationshipId)}
            </select>
          </FormGroup>
          <input className={famliyMember.id == -1?"btn btn-success":"btn btn-warning"} onClick={(e) => addnewFM(e)} type="submit" value={famliyMember.id == -1?"Add":"Edit"}></input>

          <Button onClick={() => setFamliyMember(null)} >Cancel</Button>
        </Form>
      </div>
    else
      return canEdit()? <Button color="success" onClick={() => setFamliyMember({
        id: -1,
        firstName: "",
        lastName: "",
        dateOfBirth: new Date(),
        nationalityId: null,
        relationshipId: null

      })}>Add Family Memeber</Button>:"";
  }
  const getFMTable = () => {
    if (famliyMember)
      return;
    const rows = famliyMembers.map(item => <tr >
      <th scope="row">{item.id}</th>
      <td>{item.firstName}</td>
      <td>{item.lastName}</td>
      <td>{Moment(item.dateOfBirth).format('yyyy-MM-DD')}</td>
      <td>{item.relationshipId ? relationshipTypes.filter(i => i.id == item.relationshipId)[0].name : ''}</td>
      <td>{item.nationalityId ? props.nationalities.filter(i => i.nationalityId == item.nationalityId)[0].nationalityName : ''}</td>
      <td>
        {canEdit()?   <div style={{ width: "110px" }}>
          <Button color="warning" onClick={() => updateFM(item)}>Edit</Button>
          <Button color="danger" onClick={() => deleteFM(famliyMembers.indexOf(item))}>Del</Button>
        </div>:""}
     
      </td>
    </tr>

    );

    const table = <FormGroup>
      <Table responsive hover>
        <thead>
          <tr>
            <th>ID</th>
            <th>firstName</th>
            <th>lastName</th>
            <th>dateOfBirth</th>
            <th>relationship</th>
            <th>nationality</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {rows}
        </tbody>
      </Table>
    </FormGroup>


    return table
  }

  return (

    <Form onSubmit={props.id ? submitFormEdit : submitFormAdd}>

      <ul className="nav nav-tabs  mt-3" id="myTab" role="tablist">
        <li className="nav-item">
          <a className={showTabs ? "nav-link active" : "nav-link"} onClick={(e) => { e.preventDefault(); setShowTabs(true) }} id="BasicInformationx" data-toggle="tab" href="#BasicInformation" role="tab" aria-controls="BasicInformationx" aria-selected="{showTabs}">Basic Information</a>
        </li>
        <li class="nav-item">
          <a className={showTabs ? "nav-link" : "nav-link active"} onClick={(e) => { e.preventDefault(); setShowTabs(false) }} class="nav-link" id="FamliyMembersx" data-toggle="tab" href="#FamliyMembers" role="tab" aria-controls="FamliyMembersx" aria-selected="{!showTabs}">FamliyMembers</a>
        </li>
      </ul>



      <div id="BasicInformation" style={{ display: showTabs ? 'block' : 'none' }}>
        <FormGroup>
          <Label for="firstName">First Name</Label>
          <Input required   disabled={!canEdit()?"disabled":""}
            type="text"
            name="firstName"
            id="firstName"
            onChange={onChange}
            value={student.firstName === null ? "" : student.firstName}
          />
        </FormGroup>
        <FormGroup>
          <Label for="lastName">Last Name</Label>
          <Input  disabled={!canEdit()?"disabled":""}
            req
            type="text"
            required
            name="lastName"
            id="lastName"
            onChange={onChange}
            value={student.lastName === null ? "" : student.lastName}
          />
        </FormGroup>
        <FormGroup>
          <Label for="dateOfBirth">Date Of Birth</Label>
          <Input required  disabled={!canEdit()?"disabled":""}
            type="Date"
            name="dateOfBirth"
            id="dateOfBirth"
            onChange={onChange}
            value={student.dateOfBirth === null ? new Date() : Moment(student.dateOfBirth).format('yyyy-MM-DD')}
          />
        </FormGroup>
        <FormGroup>
          <Label for="nationalityId">nationality</Label>
          <select required className="form-control" id='nationalityId' name='nationalityId' onChange={onChange}  disabled={!canEdit()?"disabled":""}>

            {getnationalitiesSelect(student.nationalityId)}
          </select>
        </FormGroup>
      </div>
      <div id="FamliyMembers" style={{ display: !showTabs ? 'block' : 'none' }}>
        {getFMForm()}
        {getFMTable()}

      </div> 
 {getSaveButton()}
    </Form>
  );
}

export default AddEditForm;
