const apiUrl = "https://localhost:5001";


export const addRegimenExercise = (regimenExerciseObject) => {
    return fetch(`${apiUrl}/api/RegimenExercise`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(regimenExerciseObject),
    })
    .then((r) => {
        if (!r.ok) {
            throw new Error("Failed to create regimen exercise!")
        }
        return r.json();
    });
};

export const editRegimenExercise = (regimenExercise) => {
    return fetch(`${apiUrl}/api/RegimenExercise/${regimenExercise.id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(regimenExercise)
    })
}

export const deleteRegimenExercise = (id) => {
    return fetch(`${apiUrl}/api/RegimenExercise/${id}`, {
        method: "DELETE",
    })
    // will need to redirect somewhere .then(getAllRegimens)
}