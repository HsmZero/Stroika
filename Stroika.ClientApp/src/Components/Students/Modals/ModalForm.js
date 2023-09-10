import React, { useState } from "react";
import { Button, Modal, ModalHeader, ModalBody } from "reactstrap";
import AddEditForm from "./Forms/FormAddEdit";
import store from '../../../reducer'
function ModalForm(props, ref) {
  const [modal, setModal] = useState(false);
  const [role, setRole] = useState(store.getState().role);
  const toggle = () => {
    setModal(!modal);
  };
  store.subscribe(() => {
    const state=store.getState();
    setRole(state.role);
    if(state.closeEditModel)
    setModal(false);
  });
  

  const closeBtn = (
    <button className="close" onClick={toggle}>
      &times;
    </button>
  );
  const label = props.buttonLabel;
  const  isEditMpde=label === "Edit";
  let button = "";
  let title = "";

  if (label === "Edit") {
    button = (
      <Button
        color="warning"
        onClick={toggle}
        style={{ float: "left", marginRight: "10px" }}
      >
        {label}
      </Button>
    );
    title = "Edit Student";
  } else {
    button = (
      <Button
        color="success"
        onClick={toggle}
        style={{ float: "left", marginRight: "10px" }}
      >
        {label}
      </Button>
    );
    title = "Add New Student";
  }
  

  return (
    <div>
      {button}
      <Modal dialogClassName="my-modal"
        isOpen={modal}
        toggle={toggle}
        className={props.className}
        backdrop={"static"}
        keyboard={false} 
      >
        <ModalHeader toggle={toggle} close={closeBtn}>
          {title}
        </ModalHeader>
        <ModalBody>
          <AddEditForm
            addItemToState={props.addItemToState}
            updateState={props.updateState}
            toggle={toggle}
            id={props.id}
            nationalities={props.nationalities}
          />
        </ModalBody>
      </Modal>
    </div>
  );
}
 

export default  ModalForm