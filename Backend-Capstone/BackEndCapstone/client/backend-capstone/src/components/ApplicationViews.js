import React from "react";
import { Route, Routes } from "react-router-dom";
import Provider from "./Provider";
import Patient from "./Patient";
import Admin from "./Admin";

export default function ApplicationViews() {
  const user = JSON.parse(localStorage.getItem("user"));
  return (
    //Primary route reassignment based on if the user is admin, pt, or provider to allow differing dashboards
    <Routes>
        <Route path="/" >
          {user.userType.type === "Admin" ? (
            <Route path="/" element={<Admin />} />
          ) : (
            user.userType.type === "Patient" ? (
              <Route path="/" element={<Patient />} />
            ) : (
              user.userType.type === "Provider" ? (
                <Route path="/" element={<Provider />} />
              ) : (
                <></>
              )
            )
          )}
        </Route>
    </Routes>
  );

}