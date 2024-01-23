import React, { useState } from "react";
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import TaskAltIcon from '@mui/icons-material/TaskAlt';
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { toast } from "react-toastify";
import { ILogin } from "../interfaces/ILogin";

import './styles/login.css';

export default function Login() {
    const [email, setEmail] = useState<String>("");
    const [password, setPassword] = useState<String>("");

    const navigate = useNavigate();

    const submitRequestLogin = async () => {
        if (email != "" && password != "") {
            let bodyLogin : ILogin = {
                email,
                password
            };

            axios.post("https://localhost:7001/api/Auth/login", bodyLogin)
                .then(resp => {
                    if (resp.status === 200 && resp.data.authenticated === true) {
                        sessionStorage.setItem("token", resp.data.token);
                        sessionStorage.setItem("expiration", resp.data.expiration);

                        navigate("/home")
                    } else {
                        toast.success("Não foi possível realizar o Login!");
                    }
                })
                .catch(error => toast.error(error.response.data.Erro[0]));
        }
    }

    return (
        <React.Fragment>
            <div className="page-login">
                <div className="logo-task">
                    <TaskAltIcon sx={{
                        width: "150px",
                        height: "150px",
                        marginBottom: "20px"
                    }} />
                </div>
                <div className="form-login">
                    <TextField
                        label="E-mail:"
                        variant="outlined"
                        value={email}
                        onChange={e => setEmail(e.target.value)} />

                    <TextField
                        label="Senha:"
                        variant="outlined"
                        type="password"
                        value={password}
                        onChange={e => setPassword(e.target.value)} />

                    <Button
                        variant="contained"
                        onClick={() => submitRequestLogin()}>
                        Acessar
                    </Button>
                    <Button variant="contained"
                        onClick={()=> navigate("register")}>
                        Registrar-se
                    </Button>
                </div>

                <small>2024 Copyright © - Desenvolvido por eXtend File.</small>
            </div>
        </React.Fragment>
    );
}