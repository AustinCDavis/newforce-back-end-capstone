const apiUrl = "https://localhost:5001";

export const getAllExercises = () => {
    return fetch(`${apiUrl}/api/Exercise`)
    .then((r) => r.json())
};

export const addExercise = (exerciseObject) => {
    return fetch(`${apiUrl}/api/Exercise`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(exerciseObject),
    })
    .then((r) => {
        if (!r.ok) {
            throw new Error("Failed to create exercise")
        }
        return r.json();
    });
};

export const getExerciseById = (id) => {
    return fetch(`${apiUrl}/api/Exercise/${id}`)
    .then((r) => r.json())

}

export const getExercisesByPatientId = (id) => {
    return fetch(`${apiUrl}/api/Exercise/Patient${id}`)
    .then((r) => r.json())

}

export const getExercisesByRegimenId = (id) => {
    return fetch(`${apiUrl}/api/Exercise/Regimen${id}`)
    .then((r) => r.json())

}

export const editExercise = (exercise) => {
    return fetch(`${apiUrl}/api/Exercise/${exercise.id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(exercise)
    })
}

export const deleteExercise = (id) => {
    return fetch(`${apiUrl}/api/Exercise/${id}`, {
        method: "DELETE",
    }).then(getAllExercises)
}