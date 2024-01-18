import React from "react";
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import TaskAltIcon from '@mui/icons-material/TaskAlt';

import './styles/login.css';

export default function Login() {
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
                    <TextField label="E-mail:" variant="outlined" />
                    <TextField label="Senha:" variant="outlined" type="password" />
                    <Button variant="contained">Acessar</Button>
                    <Button variant="contained">Registrar-se</Button>
                </div>
            </div>
        </React.Fragment>
    );
}