import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { addRegimen, deleteRegimen, getRegimenById, editRegimen, getRegimensByProviderId } from "../../Managers/RegimenManager";
import { Container, Table } from "reactstrap";
import "./Regimen.css"
import { EyeFill, PencilFill, TrashFill } from "react-bootstrap-icons";
import { Button, Modal, Form, Stack, Accordion, Card } from "react-bootstrap";
import { getExercisesByRegimenId } from "../../Managers/ExerciseManager";


export const RegimenList = () => {
    const navigate = useNavigate();
    const userObject = JSON.parse(localStorage.getItem("user"));
    const [show, setShow] = useState(false);
    const [show2, setShow2] = useState(false);
    const [show3, setShow3] = useState(false);
    const [regimens, setRegimens] = useState([]);
    const [exercises, setExercises] = useState([]);
    const [newRegimen, setNewRegimen] = useState({
        title: "",
        description: "",
        providerProfileId: userObject.id
    });
    const [regimen, updateRegimen] = useState({
        title: "",
        description: "",
        providerProfileId: userObject?.id
    })

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    const handleClose2 = () => setShow2(false);
    const handleShow2 = () => setShow2(true);
    const handleClose3 = () => setShow3(false);
    const handleShow3 = () => setShow3(true);

    const getRegimens = () => {
        getRegimensByProviderId(userObject.id).then(allRegimens => setRegimens(allRegimens));
    };

    useEffect(() => {
        getRegimens();
    }, []);

    const handleFieldChange = (evt) => {
        const stateToChange = { ...newRegimen };
        stateToChange[evt.target.id] = evt.target.value;
        setNewRegimen(stateToChange);
    };

    const handleEditOpenClick = (regimen) => {

        getRegimenById(regimen.id).then(regimenData => updateRegimen(regimenData))
        return (
            handleShow2()
        );

    };

    const handleDetailOpenClick = (regimen) => {

        getRegimenById(regimen.id).then(regimenData => updateRegimen(regimenData));
        getExercisesByRegimenId(regimen.id).then(exerciseDate => setExercises(exerciseDate));
        return (
            handleShow3()
        );

    };

    const handleAddSubmit = (event) => {
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
                    handleClose();
                    getRegimens();
                })
        );

    };

    const handleEditSubmit = (event) => {
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
                handleClose2();
                getRegimens();
            })
    };

    const handleDelete = (regimen) => {
        const confirmBox = window.confirm(`Do you really want to delete ${regimen?.title} regimen?`)
        if (confirmBox === true) {
            deleteRegimen(regimen.id)
                .then(() => {
                    getRegimens();
                });
        }
    };


    //returns a list of all user regimens
    return (
        <>
            {/* Modals for edit/create/details */}
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>New Regimen</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
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
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" type="submit" onClick={handleAddSubmit}>
                        Submit
                    </Button>
                </Modal.Footer>
            </Modal>

            <Modal show={show2} onHide={handleClose2}>
                <Modal.Header closeButton>
                    <Modal.Title>Edit Regimen</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group>
                            <Form.Label>Title</Form.Label>
                            <Form.Control type="text" id="title" value={regimen.title} onChange={
                                (evt) => {
                                    const copy = { ...regimen }
                                    copy.title = evt.target.value
                                    updateRegimen(copy)
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
                                        const copy = { ...regimen }
                                        copy.description = evt.target.value
                                        updateRegimen(copy)
                                    }}
                            />
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose2}>
                        Close
                    </Button>
                    <Button variant="primary" type="submit" onClick={handleEditSubmit}>
                        Submit
                    </Button>
                </Modal.Footer>
            </Modal>

            <Modal show={show3} onHide={handleClose3} size="lg">
                <Modal.Header closeButton>
                    <Modal.Title>Regimen Details</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Card >
                        <Card.Header>{regimen.title}</Card.Header>
                        <Card.Body>
                            <Card.Text>
                                {regimen.description}
                            </Card.Text>
                                {
                                (exercises.length > 0) ? 
                                (exercises.map((exercise) =>
                            <Accordion>
                                    <Accordion.Item eventKey={0} key={exercise.id}>
                                        <Accordion.Header>{exercise.name}</Accordion.Header>
                                        <Accordion.Body>
                                            <Stack direction="horizontal">
                                                <Accordion.Body>
                                                    <Card.Header >Instructions:</Card.Header>
                                                    <Card.Body border="secondary" style={{ whiteSpace: "pre-line" }}>
                                                    {exercise.instructions}
                                                    </Card.Body>
                                                </Accordion.Body>
                                                <Card.Img variant="top" src={exercise.videoLocation} />
                                            </Stack>
                                        </Accordion.Body>
                                    </Accordion.Item>
                            </Accordion>
                                )) : (
                                    <Card.Header className="text-center">No exercises associated at this time. <br/> Please select the button below to add an existing exercise or to create new exercises to associate.</Card.Header>
                                )
                            }

                        </Card.Body>
                        <Card.Footer className="text-muted text-center">
                            <Button onClick={() => navigate(`/Regimens/${regimen.id}`)}>Add or Create Exercise</Button>
                        </Card.Footer>
                    </Card>
                </Modal.Body>
            </Modal>
            {/* end of modals */}
            <Button onClick={handleShow}> New Regimen </Button>
            <Container>
                <Table striped size="sm" className="table_index" id="regimenTable">
                    <thead>
                        <tr>
                            <th>Regimen</th>
                            <th>Description</th>
                            <th>Date Created</th>
                            <th>Creator</th>
                            <th className="text-center">Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {regimens.map((regimen) => (
                            <>
                                <tr key={regimen.id}>
                                    <td>{regimen.title}</td>
                                    <td>{regimen.description}</td>
                                    <td>{regimen.createDateTime}</td>
                                    <td>{regimen.userProfile.fullName}</td>
                                    <td>
                                        <div className="function-container">
                                            <Stack direction="horizontal">
                                                <Button onClick={() => handleDetailOpenClick(regimen)} variant="outline-secondary" className="x-1" title="Details">
                                                    <EyeFill />
                                                </Button>
                                                <Button onClick={() => handleEditOpenClick(regimen)} variant="outline-secondary" className="btn btn-outline-secondary mx-1" title="Edit">
                                                    <PencilFill />
                                                </Button>
                                                <Button onClick={() => handleDelete(regimen)}
                                                    variant="outline-secondary" className="mx-1" title="Delete">
                                                    <TrashFill />
                                                </Button>
                                            </Stack>
                                        </div>
                                    </td>
                                </tr>
                            </>
                        ))}
                    </tbody>
                </Table>
            </Container>
        </>
    );


};