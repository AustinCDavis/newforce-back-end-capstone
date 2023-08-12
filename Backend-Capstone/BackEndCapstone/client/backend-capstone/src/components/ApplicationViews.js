import React from "react";
import { Route, Routes } from "react-router-dom";
import Provider from "./Provider";
import Patient from "./Patient";
import Admin from "./Admin";
import { UserProfileList } from "./UserProfile.js/UserProfileList";
import { UserProfileDetails } from "./UserProfile.js/UserProfileDetails";
import { RegimenList } from "./Regimen.js/RegimenList";
import { PatientAssignmentList } from "./PatientAssignment.js/PatientAssignmentList";
import { PatientChart } from "./PatientAssignment.js/PatientChart";

export default function ApplicationViews() {
  const user = JSON.parse(localStorage.getItem("user"));
  return (
    //Primary route reassignment based on if the user is admin, pt, or provider to allow differing dashboards
    <Routes>
        <Route path="/" >
          {user.userType.type === "Admin" ? (
            <>
            <Route path="/" element={<Admin />} />
            <Route path="/UserProfiles" element={<UserProfileList />} />

            </>
          ) : (
            user.userType.type === "Patient" ? (
              <>
              <Route path="/" element={<Patient />} />
              </>
            ) : (
              user.userType.type === "Provider" ? (
                <>
                <Route path="/" element={<Provider />} />
                <Route path="/Regimens" element={<RegimenList />} />
                <Route path="/PatientAssignments" element={<PatientAssignmentList />} />
                <Route path="/PatientAssignments/:id" element={<PatientChart />} />
                </>
              ) : (
                <></>
              )
            )
          )}
        </Route>
        <Route path="/UserProfile/:id" element={<UserProfileDetails />} />
    </Routes>
  );

}