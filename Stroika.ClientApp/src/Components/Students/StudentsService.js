 
 import { environment } from "../../environment"; 
 import axios from 'axios';
    
 
export  const StudentsService = {
    GetAllStudents : () =>    axios.get(environment.appURL+"Students"),
    AddNewStudent : (student) =>    axios.post(environment.appURL+"Students",student),
    GetAllNationalities : () =>    axios.get(environment.appURL+"Nationalities"),
    DeleteStudent : (studentId) =>    axios.delete(environment.appURL+"Students/"+studentId),
    UpdateStudent : (studentId,student) =>    axios.put(environment.appURL+"Students/"+studentId,student),
    GetStudentNationality : (studentId) =>    axios.get(environment.appURL+"Students/"+studentId+"/Nationality"),
    updateStudentNationality : (studentId,nationalityId) =>    axios.put(environment.appURL+"Students/"+studentId+"/Nationality"+nationalityId),
    GetStudentFullDataById: (studentId) => axios.get(environment.appURL+"Students/GetStudentFullDataById/"+studentId),
    AddNewStudentWithFamliy : (student) =>    axios.post(environment.appURL+"Students/AddNewStudentWithFamliy",student),
   UpdateStudentWithFamliy : (student) =>    axios.put(environment.appURL+"Students/AddNewStudentWithFamliy",student),
  
}
 
 