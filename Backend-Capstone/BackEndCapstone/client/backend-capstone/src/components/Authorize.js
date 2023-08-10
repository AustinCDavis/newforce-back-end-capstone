import React from "react"
import { Route, Routes, Navigate } from "react-router-dom";
import Login from "./Login";
import Register from "./Register";
import LandingPage from "./LandingPage";

export default function Authorize({ setIsLoggedIn }) {

    return (
        <Routes>
            <Route path="/login" element={<Login setIsLoggedIn={setIsLoggedIn} />} />
            <Route path="/register" element={<Register setIsLoggedIn={setIsLoggedIn} />} />
            <Route path="/landingPage" element={<LandingPage />} />
            
            <Route path="*" element={<Navigate to="/landingPage" />} />
        </Routes>
    );
}