import React, { useState, useEffect } from "react";
import { Col, Card, Container, Row, ListGroup, Button, Carousel } from "react-bootstrap";
import { useParams, useNavigate } from "react-router-dom";
import dateFormat from "dateformat";
import { getUserProfileById } from "../../Managers/UserProfileManager";
import { getRegimenByPatientId } from "../../Managers/RegimenManager";
import { getExercisesByPatientId } from "../../Managers/ExerciseManager";
import { getNotesByPatientId } from "../../Managers/NoteManager";
import "./PatientAssignment.css"
import 'bootstrap/dist/css/bootstrap.min.css'

export const PatientChart = () => {
    const [patient, setPatient] = useState([]);
    const [regimen, setRegimen] = useState([]);
    const [exercises, setExercises] = useState([]);
    const navigate = useNavigate();
    const [notes, setNotes] = useState([]);

    const { id } = useParams();

    useEffect(() => {
        getUserProfileById(id)
            .then(patient => setPatient(patient));
    }, [id]);

    useEffect(() => {
        getRegimenByPatientId(id)
            .then(regimen => setRegimen(regimen));
    }, [id]);

    useEffect(() => {
        getExercisesByPatientId(id)
            .then(exercises => setExercises(exercises));
    }, [id])

    useEffect(() => {
        getNotesByPatientId(id)
            .then(notes => setNotes(notes));
    }, [id])





    //returns a list of all user patientAssignments
    return (
        <Container fluid >
            <h1 className="mb-4 text-center" style={{ margin: 0 }}> Patient Profile</h1>
            <Row>
                <Col xs={3} style={{ marginLeft: 5 }}>
                    <Card>
                        <Card.Img variant="Top" src={patient.imageLocation} />
                        <Card.Body>
                            <Card.Title>{patient.fullName}</Card.Title>
                            <Card.Text>
                                Some quick example text to build on the card title and make up the
                                bulk of the card's content.
                            </Card.Text>
                        </Card.Body>
                        <ListGroup className="list-group-flush">
                            <ListGroup.Item>Cras justo odio</ListGroup.Item>
                            <ListGroup.Item>Dapibus ac facilisis in</ListGroup.Item>
                            <ListGroup.Item>Vestibulum at eros</ListGroup.Item>
                        </ListGroup>
                        <Card.Body>
                            <Card.Link href="#">Card Link</Card.Link>
                            <Card.Link href="#">Another Link</Card.Link>
                        </Card.Body>
                    </Card>

                </Col>
                <Col xs={9} style={{ marginLeft: -5 }}>
                    <Card style={{ marginBottom: 10 }}>
                        <Card.Header className="text-center">Last Patient Note</Card.Header>
                        <Card.Body>
                            <blockquote className="blockquote mb-0">
                                <p>
                                    {' '}
                                    {notes[0]?.content}
                                    {' '}
                                </p>
                                <footer className="blockquote-footer">
                                    <i>{dateFormat(notes[0]?.createDateTime, "dddd, mmmm dS, yyyy, h:MM:ss TT")}</i>
                                </footer>
                            </blockquote>
                        </Card.Body>
                        <Card.Footer className="text-center"><Button variant="primary" onClick={() => navigate(`/PatientAssignments/${id}/Notes`)}>All Notes</Button></Card.Footer>
                    </Card>
                    <Card className="text-center">
                        <Card.Header>Current Patient Regimen</Card.Header>
                        <Card.Body>
                            <Card.Title>{regimen[0]?.title}</Card.Title>
                            <Card.Text>
                                {regimen[0]?.description}
                            </Card.Text>
                            <Carousel >
                                {exercises.map((exercise) => (
                                    <Carousel.Item>
                                        <Card>
                                            <Card.Body>
                                                <Card.Title>{exercise?.name}</Card.Title>
                                            </Card.Body>
                                            <ListGroup className="list-group-flush">
                                                <ListGroup.Item>Type: {exercise?.type}</ListGroup.Item>
                                                <ListGroup.Item>Muscle Group: {exercise?.muscle}</ListGroup.Item>
                                            </ListGroup>
                                            <Card.Footer>
                                                <Card.Link href="#">View Exercise</Card.Link>
                                            </Card.Footer>
                                        </Card>
                                    </Carousel.Item>
                                ))}
                            </Carousel>
                        </Card.Body>
                        <Card.Footer className="text-muted"><Button variant="primary">View All Exercises</Button></Card.Footer>
                    </Card>
                </Col>
            </Row>

            <hr className="mt-5" />
            <div className="clearfix"></div>
        </Container>
    );


};