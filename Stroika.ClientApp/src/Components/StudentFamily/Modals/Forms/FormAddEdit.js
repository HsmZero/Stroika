import React, { useState, useEffect } from "react";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";
import Moment from 'moment';
function AddEditForm(props) {
  const [form, setValues] = useState({
    id: 0,
    firstName: "",
    lastName: "",
    dateOfBirth: new Date(),
    
  });

  const onChange = (e) => {
    setValues({
      ...form,
      [e.target.name]: e.target.value
    });
  };

  const submitFormAdd = (e) => {
    console.log(props.item);
    e.preventDefault();
    // fetch('http://localhost:3000/crud', {
    //   method: 'post',
    //   headers: {
    //     'Content-Type': 'application/json'
    //   },
    //   body: JSON.stringify({
    //     first: form.first,
    //     last: form.last,
    //     email: form.email,
    //     phone: form.phone,
    //     location: form.location,
    //     hobby: form.hobby
    //   })
    // })
    //   .then(response => response.json())
    //   .then(item => {
    //     if(Array.isArray(item)) {
    //       props.addItemToState(item[0])
    //       props.toggle()
    //     } else {
    //       console.log('failure')
    //     }
    //   })
    //   .catch(err => console.log(err))
    props.addItemToState(form);
    props.toggle();
  };

  const submitFormEdit = (e) => {
    e.preventDefault();
    // fetch("http://localhost:3000/crud", {
    //   method: "put",
    //   headers: {
    //     "Content-Type": "application/json"
    //   },
    //   body: JSON.stringify({
    //     id: form.id,
    //     first: form.first,
    //     last: form.last,
    //     email: form.email,
    //     phone: form.phone,
    //     location: form.location,
    //     hobby: form.hobby
    //   })
    // })
    //   .then((response) => response.json())
    //   .then((item) => {
    //     if (Array.isArray(item)) {
    //       // console.log(item[0])
    //       props.updateState(item[0]);
    //       props.toggle();
    //     } else {
    //       console.log("failure");
    //     }
    //   })
    //   .catch((err) => console.log(err));
    props.updateState(form);
    props.toggle();
  };

  useEffect(() => {
    if (props.item) {
      const { id, firstName, lastName, dateOfBirth  } = props.item;
      setValues({ id, firstName, lastName, dateOfBirth });
    }
  }, [props.item]);

  return (
    <Form onSubmit={props.item ? submitFormEdit : submitFormAdd}>
      <FormGroup>
        <Label for="firstName">First Name</Label>
        <Input
          type="text"
          name="firstName"
          id="firstName"
          onChange={onChange}
          value={form.firstName === null ? "" : form.firstName}
        />
      </FormGroup>
      <FormGroup>
        <Label for="lastName">Last Name</Label>
        <Input
          type="text"
          req
          name="lastName"
          id="lastName"
          onChange={onChange}
          value={form.lastName === null ? "" : form.lastName}
        />
      </FormGroup>
      <FormGroup>
        <Label for="dateOfBirth">Date Of Birth</Label>
        <Input
          type="Date"
          name="dateOfBirth"
          id="dateOfBirth"
          onChange={onChange}
          value={form.dateOfBirth === null ? new Date() : Moment(form.dateOfBirth).format('yyyy-MM-DD')}
        />
      </FormGroup>
      <Button>Submit</Button>
    </Form>
  );
}

export default AddEditForm;
