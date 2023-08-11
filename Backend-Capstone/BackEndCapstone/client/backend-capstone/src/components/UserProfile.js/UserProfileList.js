import React, { useState, useEffect} from "react";
import { useNavigate } from "react-router-dom";
import { deleteUserProfile, getAllUserProfiles } from "../../Managers/UserProfileManager";
import { Container, NavLink, Table } from "reactstrap";
import "./UserProfile.css"
import { EyeFill } from "react-bootstrap-icons";
import { PencilFill} from "react-bootstrap-icons"
import { TrashFill} from "react-bootstrap-icons"

export const UserProfileList = () => {
    const [profiles, setProfiles] = useState([]);
    const navigate = useNavigate();

    const getProfiles = () => {
        getAllUserProfiles().then(allProfiles => setProfiles(allProfiles));
    };

    useEffect(() => {
        getProfiles();
    }, []);

    
    const handleDelete = (profile) => {
        const confirmBox = window.confirm(`Do you really want to delete ${profile?.firstName} ${profile?.lastName}'s profile?`)
        if (confirmBox === true){
            deleteUserProfile(profile.id)
            .then(() => {
                getProfiles();
                navigate('/')
            });
        }
    };


    //returns a list of all user profiles
    return (
        <Container>

        <Table striped size="sm" className="table_index" id="userProfileTable">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Full Name</th>
                    <th>User Eamil</th>
                    <th>User Type</th>
                    <th>Actions</th>
                </tr>
                </thead>
                <tbody>
                    {profiles.map((profile) => (
                        <tr key={profile.id}>
                            <th scope="row">{profile.id}</th>
                            <td><NavLink href={`/userprofile/${profile.id}`} id="userDetailsLink"><u>{profile.fullName}</u></NavLink></td>
                            <td>{profile.email}</td>
                            <td>{profile.userType.type}</td>
                            <td>
                        <div className="function-container">
                        <a href={`/UserProfile/${profile.id}`} className="btn btn-outline-secondary mx-1" title="Details">
                            <EyeFill />
                        </a>
                        <a href="/" className="btn btn-outline-secondary mx-1" title="Edit">
                            <PencilFill />
                        </a>
                        <button onClick={() => handleDelete(profile)} className="btn btn-outline-secondary mx-1" title="Delete">
                            <TrashFill/>
                        </button>
                        </div>
                    </td>
                        </tr>
                    ))}
            </tbody>
        </Table>
                    </Container>
    );


};