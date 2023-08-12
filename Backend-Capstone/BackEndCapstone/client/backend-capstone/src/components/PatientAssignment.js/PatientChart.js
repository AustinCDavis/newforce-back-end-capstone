import React, { useState, useEffect } from "react";
import { Col, Card, Container, Row, ListGroup, Button, Carousel } from "react-bootstrap";
import "./PatientAssignment.css"
import { useParams } from "react-router-dom";
import { getUserProfileById } from "../../Managers/UserProfileManager";
import 'bootstrap/dist/css/bootstrap.min.css'


export const PatientChart = () => {
    const [patient, setPatient] = useState([]);
    const { id } = useParams();

    const getPatient = () => {

        console.log(id)
        getUserProfileById(id)
            .then(patient => setPatient(patient));
    };

    useEffect(() => {
        getPatient();
    }, []);


    //returns a list of all user patientAssignments
    return (
        <Container fluid >
            <h1 className="mb-4" style={{ margin: 0 }}> Patient Profile</h1>
            <Row>
                <Col xs={3} style={{ marginLeft: 5 }}>
                    <Card>
                        <Card.Img style={{ marginTop: '-3.5rem' }} variant="middle" src={patient.imageLocation} />
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
                            <Card.Title>Special title treatment</Card.Title>
                            <Card.Text>
                                With supporting text below as a natural lead-in to additional content.
                            </Card.Text>
                            <Button variant="primary">All Notes</Button>
                        </Card.Body>
                        <Card.Footer className="text-center text-muted">2 days ago</Card.Footer>
                    </Card>
                    <Card className="text-center">
                        <Card.Header>Patient Regimen</Card.Header>
                        <Card.Body>
                            <Card.Title>Special title treatment</Card.Title>
                            <Card.Text>
                                With supporting text below as a natural lead-in to additional content.
                            </Card.Text>
                            <Carousel slide={false}>
                                <Carousel.Item>
                                    <Card>
                                        <Card.Body>
                                            <Card.Title>A</Card.Title>
                                            <Card.Text>
                                                Some quick example text to build on the card title and make up the
                                                bulk of the card's content.
                                            </Card.Text>
                                        </Card.Body>
                                        <ListGroup className="list-group-flush">
                                            <ListGroup.Item>Cras justo odio</ListGroup.Item>
                                        </ListGroup>
                                        <Card.Body>
                                            <Card.Link href="#">Card Link</Card.Link>
                                            <Card.Link href="#">Another Link</Card.Link>
                                        </Card.Body>
                                    </Card>
                                </Carousel.Item>
                                <Carousel.Item>
                                    <Card>
                                        <Card.Body>
                                            <Card.Title>Card</Card.Title>
                                            <Card.Text>
                                                Some quick example text to build on the card title and make up the
                                                bulk of the card's content.
                                            </Card.Text>
                                        </Card.Body>
                                        <ListGroup className="list-group-flush">
                                            <ListGroup.Item>Cras justo odio</ListGroup.Item>
                                        </ListGroup>
                                        <Card.Body>
                                            <Card.Link href="#">Card Link</Card.Link>
                                            <Card.Link href="#">Another Link</Card.Link>
                                        </Card.Body>
                                    </Card>
                                </Carousel.Item>
                                <Carousel.Item>
                                    <Card>
                                        <Card.Body>
                                            <Card.Title>Title</Card.Title>
                                            <Card.Text>
                                                Some quick example text to build on the card title and make up the
                                                bulk of the card's content.
                                            </Card.Text>
                                        </Card.Body>
                                        <ListGroup className="list-group-flush">
                                            <ListGroup.Item>Cras justo odio</ListGroup.Item>
                                        </ListGroup>
                                        <Card.Body>
                                            <Card.Link href="#">Card Link</Card.Link>
                                            <Card.Link href="#">Another Link</Card.Link>
                                        </Card.Body>
                                    </Card>
                                </Carousel.Item>
                            </Carousel>
                        </Card.Body>
                        <Card.Footer className="text-muted"><Button variant="primary">View Exercises</Button></Card.Footer>
                    </Card>
                </Col>
            </Row>

            <hr class="mt-5" />
            <div class="clearfix"></div>
        </Container>
    );


};