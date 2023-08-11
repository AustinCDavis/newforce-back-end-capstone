const apiUrl = "https://localhost:5001";


export const addComment = (commentObject) => {
    return fetch(`${apiUrl}/api/Comment`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(commentObject),
    })
    .then((r) => {
        if (!r.ok) {
            throw new Error("Failed to create comment")
        }
        return r.json();
    });
};

export const getCommentByExerciseId = (id) => {
    return fetch(`${apiUrl}/api/Comment/Exercise${id}`)
    .then((r) => r.json())

}

export const editComment = (commentObject) => {
    return fetch(`${apiUrl}/api/Comment/${commentObject.id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(commentObject),
    })
}

export const deleteComment = (id) => {
    return fetch(`${apiUrl}/api/Comment/${id}`, {
        method: "DELETE",
    })
    .then(getCommentByExerciseId)
}