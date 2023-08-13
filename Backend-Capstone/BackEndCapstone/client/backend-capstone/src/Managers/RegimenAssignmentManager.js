const apiUrl = "https://localhost:5001";


export const addRegimenAssignment = (regimenAssignmentObject) => {
    return fetch(`${apiUrl}/api/RegimenAssignment`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(regimenAssignmentObject),
    })
    .then((r) => {
        if (!r.ok) {
            throw new Error("Failed to create regimen assignment!")
        }
        return r.json();
    });
};

export const editRegimenAssignment = (regimenAssignment) => {
    return fetch(`${apiUrl}/api/RegimenAssignment/${regimenAssignment.id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(regimenAssignment)
    })
}

export const deleteRegimenAssignment = (id) => {
    return fetch(`${apiUrl}/api/RegimenAssignment/${id}`, {
        method: "DELETE",
    })
    // will need to redirect somewhere .then(getAllRegimens)
}