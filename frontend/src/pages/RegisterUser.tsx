import React, { useState } from "react";
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import TaskAltIcon from '@mui/icons-material/TaskAlt';
import Tooltip from '@mui/material/Tooltip';
import { useNavigate } from "react-router-dom";
import './styles/register.css';
import axios from "axios";
import { toast } from "react-toastify";
import { IRegister } from "../interfaces/IRegister";
import OutlinedCard from "../components/Card";

export default function RegisterUser() {
    const [email, setEmail] = useState<String>("");
    const [password, setPassword] = useState<String>("");
    const [confirmPassword, setConfirmPassword] = useState<String>("");

    const navigate = useNavigate();

    const submitRequestRegisterUser = async () => {
        if (email != "" && password != "" && confirmPassword != "" && (password === confirmPassword)) {
            let bodyLogin: IRegister = {
                email,
                password
            };

            axios.post("https://localhost:7001/api/Auth/register", bodyLogin)
                .then(resp => {
                    if (resp.status === 200) {
                        toast.success("Usuário registrado com sucesso!");
                        navigate("/home");
                    }
                })
                .catch(error => {
                    toast.error("Verifique todos os campos e tente novamente!");
                    console.error(error);
                });
        } else {
            toast.warn("Verifique todos os campos e tente novamente!");
        }
    }

    return (
        <React.Fragment>
            <div className="page-register">
                <OutlinedCard>
                    <div className="logo-task">
                        <TaskAltIcon sx={{
                            width: "150px",
                            height: "150px",
                            marginBottom: "20px"
                        }} />
                    </div>
                    <div className="form-register">
                        <TextField
                            label="E-mail:"
                            variant="outlined"
                            type="email"
                            value={email}
                            onChange={e => setEmail(e.target.value)} />

                        <Tooltip
                            title="A senha deve conter no mínimo 8 caracteres, um carácter maiúsculo e um carácter especial."
                            placement="bottom-start">
                            <TextField
                                label="Senha:"
                                variant="outlined"
                                type="password"
                                value={password}
                                onChange={e => setPassword(e.target.value)} />
                        </Tooltip>

                        <Tooltip
                            title="A senha deve conter no mínimo 8 caracteres, um carácter maiúsculo e um carácter especial."
                            placement="bottom-start">
                            <TextField
                                label="Confirmar Senha:"
                                variant="outlined"
                                type="password"
                                value={confirmPassword}
                                onChange={e => setConfirmPassword(e.target.value)} />
                        </Tooltip>

                        <Button
                            variant="contained"
                            onClick={() => submitRequestRegisterUser()}>
                            Registrar-se
                        </Button>
                    </div>

                    <small id="extend-file">{new Date().getFullYear().toString()} Copyright © - Desenvolvido por eXtend File.</small>
                </OutlinedCard>
            </div>
        </React.Fragment>
    );
}