const inputLoginEmail = document.querySelector('#input_login_email');
const inputLoginPw = document.querySelector('#input_login_pw');
const submitLoginBtn = document.querySelector('#submit_login_btn');

let httpRoute = "https://localhost:7041/";
//let request = new Request(shortAdress + "/User/LoginUser");
let headers = new Headers();

headers.append("Content-Type", "application/json; charset=utf-8");

let init = {
    method: "GET",
    headers: headers,
    credentials: "include"
};

submitLoginBtn.addEventListener('click', () => { // Start the process
    let request = new Request("https://localhost:7041/User/LoginUser/Jerry.Seinfeld%40aol.com/password");
    //let request = new Request(`https://localhost:7041/User/LoginUser/${input_login_email.value}/${input_login_pw.value}`);

    let resOff = document.querySelector('#login_res');
    resOff.textContent = "";

    fetch(request, init)
        .then(function (response) {
            return response.json();
        })
        .then(function (data) {
            let res = document.querySelector("#login_res");
            res.textContent = `${data.detail}`;
            console.log(data);
        })
        .catch(function (err) {
            console.log(err.detail);
            //    console.log("err");
        });
});
