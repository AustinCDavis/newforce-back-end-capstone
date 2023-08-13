import { createContext } from "react";

const apiUrl = "https://localhost:5001";

export const UserContext = createContext();

export const getAllUserProfiles = () => {
  return fetch(`${apiUrl}/api/UserProfile`)
  .then((r) => r.json())
}

export const getUserProfileById = (id) => { //http GET by id parameter 
  return fetch(`${apiUrl}/api/UserProfile/${id}`)
  .then((r) => r.json());
}

export const getUserProfileByEmail = (email) => {  
  return fetch(`${apiUrl}/api/UserProfile/GetByEmail?email=${email}`)
  .then((r) => r.json());
}

export const editUserProfile = (profile) => {
  return fetch(`${apiUrl}/api/UserProfile/${profile.id}`, {
      method: "PUT",
      headers: {
          "Content-Type": "application/json",
      },
      body: JSON.stringify(profile)
    })
}

export const deleteUserProfile = (id) => {
  return fetch(`${apiUrl}/api/UserProfile/${id}`, {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
  }).then((r) => {
    if (!r.ok) {
      throw new Error("Failed to delete user");
    }
    return r.ok;
  });
};

export const login = (userObject) => {
  return fetch(`${apiUrl}/api/UserProfile/GetByEmail?email=${userObject.email}`)
  .then((r) => r.json())
    .then((user) => {
      if(user.id){
        localStorage.setItem("user", JSON.stringify(user));
        return user
      }
      else{
        return undefined
      }
    });
};

export const logout = () => {
      localStorage.clear()
};

export const register = (userObject) => {
  return  fetch(`${apiUrl}/api/UserProfile`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(userObject),
  })
  .then((response) => response.json())
  .then((savedUser) => {
      return fetch(`${apiUrl}/api/UserProfile/GetByEmail?email=${savedUser.email}`)
      .then((r) => r.json())
      .then((user) => {
        if(user.id){
          localStorage.setItem("user", JSON.stringify(user));
          return user
        }
        else{
          return undefined
        }
      });
    })
};