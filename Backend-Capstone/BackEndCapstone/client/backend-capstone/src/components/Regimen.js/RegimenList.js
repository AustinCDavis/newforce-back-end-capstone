import React, { useState, useEffect} from "react";
import { useNavigate } from "react-router-dom";
import { deleteRegimen, getAllRegimens } from "../../Managers/RegimenManager";
import { Container, NavLink, Table } from "reactstrap";
import "./Regimen.css"
import { EyeFill } from "react-bootstrap-icons";
import { PencilFill} from "react-bootstrap-icons"
import { TrashFill} from "react-bootstrap-icons"

export const RegimenList = () => {
    const [regimens, setRegimens] = useState([]);
    const navigate = useNavigate();

    const getRegimens = () => {
        getAllRegimens().then(allRegimens => setRegimens(allRegimens));
    };

    useEffect(() => {
        getRegimens();
    }, []);

    
    const handleDelete = (regimen) => {
        const confirmBox = window.confirm(`Do you really want to delete ${regimen?.title} regimen?`)
        if (confirmBox === true){
            deleteRegimen(regimen.id)
            .then(() => {
                getRegimens();
                navigate('/')
            });
        }
    };


    //returns a list of all user regimens
    return (
        <Container>

        <Table striped size="sm" className="table_index" id="regimenTable">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Title</th>
                    <th>Description</th>
                    <th>Date Created</th>
                    <th>Creator</th>
                    <th>Actions</th>
                </tr>
                </thead>
                <tbody>
                    {regimens.map((regimen) => (
                        <>
                            {console.log(regimen)}
                        <tr key={regimen.id}>
                            <th scope="row">{regimen.id}</th>
                            <td><NavLink href={`/Regimen/${regimen.id}`} id="regimenDetailsLink"><u>{regimen.title}</u></NavLink></td>
                            <td>{regimen.description}</td>
                            <td>{regimen.createDateTime}</td>
                            <td>{regimen.userProfile.fullName}</td>
                            <td>
                        <div className="function-container">
                        <a href={`/Regimen/${regimen.id}`} className="btn btn-outline-secondary mx-1" title="Details">
                            <EyeFill />
                        </a>
                        <a href="/" className="btn btn-outline-secondary mx-1" title="Edit">
                            <PencilFill />
                        </a>
                        <button onClick={() => handleDelete(regimen)} className="btn btn-outline-secondary mx-1" title="Delete">
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