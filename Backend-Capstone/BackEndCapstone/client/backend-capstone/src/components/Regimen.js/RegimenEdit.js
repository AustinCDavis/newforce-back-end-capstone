import { useEffect, useState } from 'react';
import { Container, Button, Form } from 'react-bootstrap';
import { useNavigate, useParams } from 'react-router-dom';
import {  editRegimen, getRegimenById } from '../../Managers/RegimenManager';

export const UpdateRegimen = () => {
    const userObject = JSON.parse(localStorage.getItem("user"));
    const navigate = useNavigate();
    const id = useParams();

    const [regimen, setRegimen] = useState({
        title: "",
        description: "",
        providerProfileId: userObject?.id
    })

    useEffect(() => {
        getRegimenById(id.id)
        .then(
            regimenData => setRegimen(regimenData)
            )
    }, [id])

    // const handleFieldChange = (evt) => {
    //     const { name, value } = evt.target;
    //     setRegimen((prevRegimen) => ({
    //         ...prevRegimen,
    //         [name]: value })
    //     );
    // };

    const handleSubmit = (event) => {
        event.preventDefault();
        const regimenToUpdate = {
            id: regimen.id,
            title: regimen.title,
            description: regimen.description,
            createDateTime: regimen.createDateTime,
            providerProfileId: regimen.providerProfileId
        };

        return editRegimen(regimenToUpdate)
            .then(() => {
                    navigate(`/Regimens/${id.id}`)
                })
    };


    return (
        <Container className="mt-4">
            <h2>Edit Regimen</h2>
            <Form onSubmit={handleSubmit}>
                <Form.Group>
                    <Form.Label>Title</Form.Label>
                    <Form.Control type="text" id="title" value={regimen.title} onChange={
                        (evt) => {
                            const copy = {...regimen}
                            copy.title = evt.target.value
                            setRegimen(copy)
                        }} />
                </Form.Group>

                <Form.Group>
                    <Form.Label>Description</Form.Label>
                    <Form.Control
                        as="textarea"
                        id="description"
                        value={regimen.description}
                        onChange={
                            (evt) => {
                                const copy = {...regimen}
                                copy.description = evt.target.value
                                setRegimen(copy)
                            }}
                    />
                </Form.Group>

                <Button variant="primary" type="submit">
                    Submit
                </Button>
            </Form>
        </Container>
    );
}