import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { deletePatientAssignment, getPatientAssignments } from "../../Managers/PatientAssignmentManager";
import { Card, Form, Container, Stack, Modal, Table, Button } from "react-bootstrap";
import { EyeFill } from "react-bootstrap-icons";
import { PencilFill } from "react-bootstrap-icons"
import { TrashFill, BookmarkPlusFill } from "react-bootstrap-icons"
import { getAllExercises, deleteExercise, getExercisesByRegimenId, editExercise, getExerciseById, addExercise } from "../../Managers/ExerciseManager";
import { getRegimenById } from "../../Managers/RegimenManager";
import { addRegimenExercise, deleteRegimenExercise } from "../../Managers/RegimenExerciseManager";

export const ExercisesPage = () => {
    const userObject = JSON.parse(localStorage.getItem("user"));
    const [show, setShow] = useState(false);
    const [show2, setShow2] = useState(false);
    const [exercises, setExercises] = useState([]);
    const [regimen, setRegimen] = useState([]);
    const [regimenExercises, setRegimenExercises] = useState([]);
    const [newExercise, setNewExercise] = useState([]);
    const [exercise, updateExercise] = useState({
        name: "",
        type: "",
        muscle: "",
        instructions: "",
        videoLocation: ""
    })


    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    const handleClose2 = () => setShow2(false);
    const handleShow2 = () => setShow2(true);


    const navigate = useNavigate();
    const { id } = useParams();

    const getExercises = () => {
        getAllExercises().then(exerciseData => setExercises(exerciseData));
    };

    const getRegimenExercises = () => {
        getExercisesByRegimenId(id).then(exerciseData => setRegimenExercises(exerciseData));
    }

    const getRegimen = () => {
        getRegimenById(id).then(regimenData => setRegimen(regimenData));
    }

    useEffect(() => {
        getRegimen();
        getRegimenExercises();
        getExercises();
    }, []);

    const handleFieldChange = (evt) => {
        const stateToChange = { ...newExercise };
        stateToChange[evt.target.id] = evt.target.value;
        setNewExercise(stateToChange);
    };

    const handleAddClick = (exercise) => {

        const regimenExerciseToSendToAPI = {
            RegimenId: id,
            ExerciseId: exercise.id
        };

        return (
            addRegimenExercise(regimenExerciseToSendToAPI)
                .then(() => {
                    getRegimenExercises();
                })
        );

    };

    const handleAddSubmit = (event) => {
        event.preventDefault();

        const exerciseToSendToAPI = {
            Name: newExercise.name,
            Type: newExercise.type,
            Muscle: newExercise.muscle,
            Instructions: newExercise.instructions,
            VideoLocation: newExercise.videoLocation
        };

        return (
            addExercise(exerciseToSendToAPI)
                .then(() => {
                    handleClose();
                    getExercises();
                })
        );

    };



    const handleEditSubmit = (event) => {
        event.preventDefault();
        const exerciseToUpdate = {
            id: exercise.id,
            name: exercise.name,
            type: exercise.type,
            muscle: exercise.muscle,
            instructions: exercise.instructions,
            videoLocation: exercise.videoLocation
        };

        return editExercise(exerciseToUpdate)
            .then(() => {
                handleClose2();
                getRegimenExercises();
                getExercises();
            })
    };

    const handleEditOpenClick = (exercise) => {

        getExerciseById(exercise.id).then(exerciseData => updateExercise(exerciseData))
        return (
            handleShow2()
        );

    };

    const handleRemove = (exercise) => {
        const confirmBox = window.confirm(`Do you really want to remove the ${exercise?.name} exercise from the ${regimen.title} regimen?`)
        if (confirmBox === true) {
            deleteRegimenExercise(exercise.regimenExercise.id)
                .then(() => {
                    getRegimenExercises();
                    getExercises();
                });
        }
    };

    const handleDelete = (exercise) => {
        const confirmBox = window.confirm(`Do you really want to delete the ${exercise?.name} exercise?`)
        if (confirmBox === true) {
            deleteExercise(exercise.id)
                .then(() => {
                    getExercises();
                });
        }
    };

    //returns a list of all user exercises
    return (
        <>
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>New Exercise</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group>
                            <Form.Label>Name</Form.Label>
                            <Form.Control type="text" id="name" onChange={handleFieldChange} />
                        </Form.Group>

                        <Form.Group>
                            <Form.Label>Type</Form.Label>
                            <Form.Control type="text" id="type" onChange={handleFieldChange} />
                        </Form.Group>

                        <Form.Group>
                            <Form.Label>Muscle</Form.Label>
                            <Form.Control type="text" id="muscle" onChange={handleFieldChange} />
                        </Form.Group>

                        <Form.Group>
                            <Form.Label>Instructions</Form.Label>
                            <Form.Control
                                as="textarea"
                                id="instructions"
                                onChange={handleFieldChange}
                            />
                        </Form.Group>

                        <Form.Group>
                            <Form.Label>Demo</Form.Label>
                            <Form.Control type="text" id="videoLocation" onChange={handleFieldChange} />
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
                    <Modal.Title>Edit Exercise</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group>
                            <Form.Label>Name</Form.Label>
                            <Form.Control type="text" id="name" value={exercise.name} onChange={(evt) => {
                                const copy = { ...exercise }
                                copy.title = evt.target.value
                                updateExercise(copy)
                            }} />
                        </Form.Group>

                        <Form.Group>
                            <Form.Label>Type</Form.Label>
                            <Form.Control type="text" id="type" value={exercise.type} onChange={(evt) => {
                                const copy = { ...exercise }
                                copy.title = evt.target.value
                                updateExercise(copy)
                            }} />
                        </Form.Group>

                        <Form.Group>
                            <Form.Label>Muscle</Form.Label>
                            <Form.Control type="text" id="muscle" value={exercise.muscle} onChange={(evt) => {
                                const copy = { ...exercise }
                                copy.title = evt.target.value
                                updateExercise(copy)
                            }} />
                        </Form.Group>

                        <Form.Group>
                            <Form.Label>Instructions</Form.Label>
                            <Form.Control
                                as="textarea"
                                id="instructions"
                                value={exercise.instructions}
                                onChange={(evt) => {
                                    const copy = { ...exercise }
                                    copy.title = evt.target.value
                                    updateExercise(copy)
                                }}
                            />
                        </Form.Group>

                        <Form.Group>
                            <Form.Label>Demo</Form.Label>
                            <Form.Control type="text" id="videoLocation" value={exercise.videoLocation} onChange={(evt) => {
                                const copy = { ...exercise }
                                copy.title = evt.target.value
                                updateExercise(copy)
                            }} />
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

            {/* Modal End */}
            <Container>
                <h1 style={{ textAlign: "center" }}>{regimen.title}</h1>
                <Container>
                    <Button onClick={handleShow}> New Exercise </Button>

                    {
                        (regimenExercises.length > 0) ?
                            (
                                <Table striped size="sm" className="table_index" id="exerciseTable">
                                    <thead>
                                        <tr>
                                            <th>Id</th>
                                            <th>Exercise</th>
                                            <th>Muscle Group</th>
                                            <th>Instructions</th>
                                            <th>Demo</th>
                                            <th>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {regimenExercises.map((exercise) => (
                                            <tr key={exercise.id}>
                                                <th scope="row">{exercise.id}</th>
                                                <td>{exercise.name}</td>
                                                <td>{exercise.muscle}</td>
                                                <td style={{ whiteSpace: "pre-line" }}>{exercise.instructions}</td>
                                                <td><img src={exercise.videoLocation}></img></td>
                                                <td>
                                                    <div className="function-container">
                                                        <Stack direction="horizontal">
                                                            <Button variant="outline-secondary" onClick={() => handleEditOpenClick(exercise)} className="mx-1" title="Edit Exercise">
                                                                <PencilFill />
                                                            </Button>
                                                            <Button variant="outline-secondary" onClick={() => handleRemove(exercise)} className="mx-1" title="Remove Associated Exercise">
                                                                <TrashFill />
                                                            </Button>
                                                        </Stack>
                                                    </div>
                                                </td>
                                            </tr>
                                        ))}
                                    </tbody>
                                </Table>
                            ) : (
                                <>
                                    <Table striped size="sm" className="table_index" id="exerciseTable">
                                        <thead>
                                            <tr>
                                                <th>Id</th>
                                                <th>Exercise</th>
                                                <th>Muscle Group</th>
                                                <th>Instructions</th>
                                                <th>Demo</th>
                                                <th>Actions</th>
                                            </tr>
                                        </thead>
                                    </Table>
                                    <Card.Header className="text-center">No exercises associated at this time. <br /> Please add exercises from the following table.</Card.Header>
                                </>
                            )
                    }
                </Container>
                <hr />
                <br />
                <h3 style={{ textAlign: "center" }}>Available Exercises</h3>
                <Container>
                    <Table striped size="sm" className="table_index" id="exerciseTable">
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Exercise</th>
                                <th>Muscle Group</th>
                                <th>Instructions</th>
                                <th>Demo</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {exercises.map((exercise) => (
                                <>
                                    <tr key={exercise.id}>
                                        <th scope="row">{exercise.id}</th>
                                        <td>{exercise.name}</td>
                                        <td>{exercise.muscle}</td>
                                        <td style={{ whiteSpace: "pre-line" }}>{exercise.instructions}</td>
                                        <td><img src={exercise.videoLocation} alt="Image unavailable."></img></td>
                                        <td>
                                            <div className="function-container">
                                                <Stack direction="horizontal">
                                                    <Button variant="outline-secondary" onClick={() => handleAddClick(exercise)} className="mx-1" title="Add Exercise To Regimen">
                                                        <BookmarkPlusFill />
                                                    </Button>
                                                    <Button variant="outline-secondary" onClick={() => handleEditOpenClick(exercise)} className="mx-1" title="Edit Exercise">
                                                        <PencilFill />
                                                    </Button>
                                                    <Button variant="outline-secondary" onClick={() => handleDelete(exercise)} className="btn btn-outline-secondary mx-1" title="Delete">
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
                </Container >
            </Container>
        </>
    )


};
