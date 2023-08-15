const apiUrl = "https://localhost:5001";


export const addNote = (noteObject) => {
    return fetch(`${apiUrl}/api/Note`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(noteObject),
    })
    .then((r) => {
        if (!r.ok) {
            throw new Error("Failed to create note")
        }
        return r.json();
    });
};



export const getNotesByPatientId = (id) => {
    return fetch(`${apiUrl}/api/Note/Patient${id}`)
    .then((r) => r.json())

}

export const getNoteById = (id) => {
    return fetch(`${apiUrl}/api/Note/Note${id}`)
    .then((r) => r.json())

}

export const editNote = (noteObject) => {
    return fetch(`${apiUrl}/api/Note/${noteObject.id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(noteObject)
    })
}

export const deleteNote = (id) => {
    return fetch(`${apiUrl}/api/Note/${id}`, {
        method: "DELETE",
    })
    // will need to add routing .then(getPatientAssignments)
}