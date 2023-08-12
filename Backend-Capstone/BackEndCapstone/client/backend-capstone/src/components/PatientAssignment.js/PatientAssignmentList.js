import React, { useState, useEffect} from "react";
import { useNavigate } from "react-router-dom";
import { deletePatientAssignment, getPatientAssignments } from "../../Managers/PatientAssignmentManager";
import { Container, NavLink, Table } from "reactstrap";
import "./PatientAssignment.css"
import { EyeFill } from "react-bootstrap-icons";
import { PencilFill} from "react-bootstrap-icons"
import { TrashFill} from "react-bootstrap-icons"

export const PatientAssignmentList = () => {
    const [patientAssignments, setPatientAssignments] = useState([]);
    const navigate = useNavigate();

    const getAllPatientAssignments = () => {
        getPatientAssignments().then(allPatientAssignments => setPatientAssignments(allPatientAssignments));
    };

    useEffect(() => {
        getAllPatientAssignments();
    }, []);

    
    const handleDelete = (patientAssignment) => {
        const confirmBox = window.confirm(`Do you really want to delete ${patientAssignment?.patientFirstName}'s assignment?`)
        if (confirmBox === true){
            deletePatientAssignment(patientAssignment.id)
            .then(() => {
                getAllPatientAssignments();
                navigate('/')
            });
        }
    };


    //returns a list of all user patientAssignments
    return (
        <Container>

        <Table striped size="sm" className="table_index" id="patientAssignmentTable">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Provider</th>
                    <th>Patient</th>
                    <th>Begin Date</th>
                    <th>End Date</th>
                    <th>Actions</th>
                </tr>
                </thead>
                <tbody>
                    {patientAssignments.map((patientAssignment) => (
                        <>
                            
                        <tr key={patientAssignment.id}>
                            <th scope="row">{patientAssignment.id}</th>
                            <td><NavLink href={`/PatientAssignments/${patientAssignment.id}`} id="patientAssignmentDetailsLink"><u>{patientAssignment.providerProfile.firstName} {patientAssignment.providerProfile.lastName}</u></NavLink></td>
                            <td><NavLink href={`/PatientAssignments/${patientAssignment.patientProfileId}`} id="patientAssignmentDetailsLink"><u>{patientAssignment.patientProfile.firstName}{patientAssignment.patientProfile.LastName}</u></NavLink></td>
                            <td>{patientAssignment.beginDate}</td>
                            <td>{patientAssignment.endDate}</td>
                            <td>
                        <div className="function-container">
                        <a href={`/PatientAssignment/${patientAssignment.id}`} className="btn btn-outline-secondary mx-1" title="Details">
                            <EyeFill />
                        </a>
                        <a href="/" className="btn btn-outline-secondary mx-1" title="Edit">
                            <PencilFill />
                        </a>
                        <button onClick={() => handleDelete(patientAssignment)} className="btn btn-outline-secondary mx-1" title="Delete">
                            <TrashFill/>
                        </button>
                        </div>
                    </td>
                        </tr>
                        </>
                    ))}
            </tbody>
        </Table>
                    </Container>
    );


};