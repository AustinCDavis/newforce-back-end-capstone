import { Container, Row } from "react-bootstrap"
import { getRegimenById } from "../../Managers/RegimenManager";
import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";

export const RegimenDetails = () => {
    const [regimen, setRegimen] = useState([]);
    const navigate = useNavigate();
    const { id } = useParams();

    const getRegimen = () => {
        getRegimenById(id).then(regimen => setRegimen(regimen));
    };

    useEffect(() => {
        getRegimen();
    }, []);

    return (
        <Container className="mt-4">
            <Row className="text-center">
        <h1>{regimen.title}</h1>
            </Row>
            <Row>
        <h2>{regimen.description}</h2>
            </Row>
        </Container>
    )
}