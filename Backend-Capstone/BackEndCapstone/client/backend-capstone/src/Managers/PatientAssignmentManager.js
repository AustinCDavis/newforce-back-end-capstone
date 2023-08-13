const apiUrl = "https://localhost:5001";

export const getPatientAssignments = () => {
    return fetch(`${apiUrl}/api/PatientAssignment`)
    .then((r) => r.json())

}

export const addPatientAssignment = (patientAssignmentsObject) => {
    return fetch(`${apiUrl}/api/PatientAssignment`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(patientAssignmentsObject),
    })
    .then((r) => {
        if (!r.ok) {
            throw new Error("Failed to create patient sssignment")
        }
        return r.json();
    });
};



export const getPatientAssignmentsByProviderId = (id) => {
    return fetch(`${apiUrl}/api/PatientAssignment/Provider${id}`)
    .then((r) => r.json())

}

export const editPatientAssignment = (patientAssignment) => {
    return fetch(`${apiUrl}/api/PatientAssignment/${patientAssignment.id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(patientAssignment)
    })
}

export const deletePatientAssignment = (id) => {
    return fetch(`${apiUrl}/api/PatientAssignment/${id}`, {
        method: "DELETE",
    })
    .then(getPatientAssignments)
}