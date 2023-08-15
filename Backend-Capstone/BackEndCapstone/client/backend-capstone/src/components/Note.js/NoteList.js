import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { getNotesByPatientId, deleteNote, addNote, getNoteById, editNote } from "../../Managers/NoteManager";
import { Container, Table } from "reactstrap";
import "./Note.css"
import { PencilFill, TrashFill } from "react-bootstrap-icons";
import { Button, Modal, Form, Stack } from "react-bootstrap";

export const PatientNoteList = () => {
    const userObject = JSON.parse(localStorage.getItem("user"));
    const [notes, setNotes] = useState([]);
    const [show, setShow] = useState(false);
    const [show2, setShow2] = useState(false);
    const [newNote, setNewNote] = useState({
        content: "",
        providerProfileId: userObject.id,
        patientProfileId: null
    });
    const [note, updateNote] = useState({
        content: "",
        providerProfileId: userObject.id,
        patientProfileId: null
    });
    const { id } = useParams();
    const navigate = useNavigate();
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    const handleClose2 = () => setShow2(false);
    const handleShow2 = () => setShow2(true);

    const getNotes = () => {
        getNotesByPatientId(id).then(allNotes => setNotes(allNotes));
    };

    useEffect(() => {
        getNotes();
    }, []);




    const handleFieldChange = (evt) => {
        const stateToChange = { ...newNote };
        stateToChange[evt.target.id] = evt.target.value;
        setNewNote(stateToChange);
    };

    const handleDelete = (note) => {
        const confirmBox = window.confirm(`Do you really want to delete the note you created on ${note.createDateTime}?`)
        if (confirmBox === true) {
            deleteNote(note.id)
                .then(() => {
                    getNotes();
                    navigate(`/PatientAssignments/${id}/Notes`)
                });
        }
    };

    const handleSubmit = (event) => {
        event.preventDefault();

        const noteToSendToAPI = {
            Content: newNote.content,
            providerProfileId: userObject.id,
            patientProfileId: id
        };

        return (
            addNote(noteToSendToAPI)
                .then(() => {
                    handleClose();
                    getNotes();
                })
        );

    };

    const handleEditOpenClick = (note) => {

        getNoteById(note.id).then(noteData => updateNote(noteData))
        return (
            handleShow2()
        );

    };

    const handleEditSubmit = (event) => {
        event.preventDefault();

        const noteUpdate = {
            id: note.id,
            content: note.content,
            createDateTime: note.createDateTime,
            providerProfileId: userObject.id,
            patientProfileId: id
        };

        return (
            editNote(noteUpdate)
                .then(() => {
                    handleClose2();
                    getNotes();
                })
        );

    };


    //returns a list of all user notes
    return (
        <>
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>New Note</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group>
                            <Form.Label>Note</Form.Label>
                            <Form.Control
                                as="textarea"
                                id="content"
                                onChange={handleFieldChange}
                            />
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" type="submit" onClick={handleSubmit}>
                        Submit
                    </Button>
                </Modal.Footer>
            </Modal>
            <Modal show={show2} onHide={handleClose2}>
                <Modal.Header closeButton>
                    <Modal.Title>Edit note</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group>
                            <Form.Label>Note</Form.Label>
                            <Form.Control
                                as="textarea"
                                id="content"
                                value={note.content}
                                onChange={(evt) => {
                                    const copy = {...note}
                                    copy.content = evt.target.value
                                    updateNote(copy)
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

            {/* End of Modals */}
            <Button onClick={handleShow}> Add Note </Button>
            <Container>
                <Table striped size="sm" className="table_index" id="noteTable">
                    <thead>
                        <tr>
                            <th>Content</th>
                            <th>Date Created</th>
                            <th>Creator</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {notes?.map((note) => (
                            <>
                                <tr key={note.id}>
                                    <td>{note.content}</td>
                                    <td>{note.createDateTime}</td>
                                    <td>{note.providerProfile.fullName}</td>
                                    <td>
                                        <div className="function-container">
                                            <Stack direction="horizontal">
                                            <Button variant="outline-secondary" onClick={() => handleEditOpenClick(note)} className="btn btn-outline-secondary mx-1" title="Edit">
                                                <PencilFill />
                                            </Button>
                                            <Button variant="outline-secondary" onClick={() => handleDelete(note)} className="mx-1" title="Delete">
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