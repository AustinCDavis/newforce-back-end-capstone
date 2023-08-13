const apiUrl = "https://localhost:5001";


export const addMessage = (messageObject) => {
    return fetch(`${apiUrl}/api/Message`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(messageObject),
    })
    .then((r) => {
        if (!r.ok) {
            throw new Error("Failed to create message")
        }
        return r.json();
    });
};

export const getMessageByFromIdAndToId = (fromId, toId) => {
    return fetch(`${apiUrl}/api/Message/From${fromId}/To${toId}`)
    .then((r) => r.json())

}

export const getMessageByUserId = (id) => {
    return fetch(`${apiUrl}/api/Message/User${id}`)
    .then((r) => r.json())

}

// export const editMessage = (messageObject) => {
//     return fetch(`${apiUrl}/api/Message/${messageObject.id}`, {
//         method: "PUT",
//         headers: {
//             "Content-Type": "application/json",
//         },
//         body: JSON.stringify(messageObject)
//     })
// }

export const deleteMessage = (id) => {
    return fetch(`${apiUrl}/api/Message/${id}`, {
        method: "DELETE",
    })
    // will need to add routing .then(getPatientAssignments)
}