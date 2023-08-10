const apiUrl = "https://localhost:5001";

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
          console.log(user)
          localStorage.setItem("user", JSON.stringify(user));
          return user
        }
        else{
          return undefined
        }
      });
    })
};