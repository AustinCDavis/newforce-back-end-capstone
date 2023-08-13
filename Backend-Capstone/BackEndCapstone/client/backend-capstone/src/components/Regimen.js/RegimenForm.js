import { useState } from 'react';
import { Container, Button, Form } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { addRegimen } from '../../Managers/RegimenManager';

export const RegimenForm = () => {
    const userObject = JSON.parse(localStorage.getItem("user"));
    const navigate = useNavigate();

    const [newRegimen, setNewRegimen] = useState({
        title: "",
        description: "",
        providerProfileId: userObject.id
    })

    const handleFieldChange = (evt) => {
        const stateToChange = { ...newRegimen };
        stateToChange[evt.target.id] = evt.target.value;
        setNewRegimen(stateToChange);
    };

    const handleSubmit = (event) => {
        event.preventDefault();

        const regimenToSendToAPI = {
            Title: newRegimen.title,
            Description: newRegimen.description,
            CreateDateTime: newRegimen.createDateTime,
            providerProfileId: userObject.id
        };

        return (
            addRegimen(regimenToSendToAPI)
            .then(() => {
                    navigate("/Regimens")
                })
        );

    };


    return (
        <Container className="mt-4">
            <h2>Add New Regimen</h2>
            <Form onSubmit={handleSubmit}>
                <Form.Group>
                    <Form.Label>Title</Form.Label>
                    <Form.Control type="text" id="title" onChange={handleFieldChange} />
                </Form.Group>

                <Form.Group>
                    <Form.Label>Description</Form.Label>
                    <Form.Control
                        as="textarea"
                        id="description"
                        onChange={handleFieldChange}
                    />
                </Form.Group>

                <Button variant="primary" type="submit">
                    Submit
                </Button>
            </Form>
        </Container>
    );
}