const apiUrl = "https://localhost:5001";

export const getAllRegimens = () => {
    return fetch(`${apiUrl}/api/Regimen`)
    .then((r) => r.json())
};

export const addRegimen = (regimenObject) => {
    return fetch(`${apiUrl}/api/Regimen`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(regimenObject),
    })
    .then((r) => {
        if (!r.ok) {
            throw new Error("Failed to create regimen")
        }
        return r.json();
    });
};

export const getRegimenById = (id) => {
    return fetch(`${apiUrl}/api/Regimen/Regimen${id}`)
    .then((r) => r.json())

}

export const getRegimenByPatientId = (id) => {
    return fetch(`${apiUrl}/api/Regimen/Patient${id}`)
    .then((r) => r.json())

}

export const getRegimensByProviderId = (id) => {
    return fetch(`${apiUrl}/api/Regimen/Provider${id}`)
    .then((r) => r.json())

}

export const editRegimen = (regimen) => {
    return fetch(`${apiUrl}/api/Regimen/${regimen.id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(regimen)
    })
}

export const deleteRegimen = (id) => {
    return fetch(`${apiUrl}/api/Regimen/${id}`, {
        method: "DELETE",
    }).then(getAllRegimens)
}