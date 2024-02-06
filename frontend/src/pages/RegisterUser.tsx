import React, { useState } from "react";
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import Tooltip from '@mui/material/Tooltip';
import { useNavigate } from "react-router-dom";
import './styles/register.css';
import axios from "axios";
import { toast } from "react-toastify";
import { IRegister } from "../interfaces/IRegister";
import OutlinedCard from "../components/Card";
import { IconButton } from "@mui/material";
import { Visibility } from "@mui/icons-material";
import { VisibilityOff } from "@mui/icons-material";
import logoExtendFile from '../assets/extendfile_branco.png';
import logoSistemaAtividade from '../assets/sistema-atividades.png'
import LoadingProgress from "../components/LoadingProgress";

export default function RegisterUser() {
    const [email, setEmail] = useState<String>("");
    const [nome, setNome] = useState<String>("");
    const [password, setPassword] = useState<String>("");
    const [confirmPassword, setConfirmPassword] = useState<String>("");
    const [showPassword, setShowPassword] = useState(false);

    const [loading, setLoading] = useState<boolean>(false);

    const handleTogglePasswordVisibility = () => {
        setShowPassword(!showPassword);
    };

    const navigate = useNavigate();

    const submitRequestRegisterUser = async () => {
        if (email != "" && nome !== "" && password != "" && confirmPassword != "" && (password === confirmPassword)) {
            setLoading(true); 

            let bodyLogin: IRegister = {
                nome,
                email,
                password
            };

            axios.post("https://localhost:7001/api/Auth/register", bodyLogin)
                .then(resp => {
                    if (resp.status === 200) {
                        setLoading(false); 
                        toast.success("Usuário registrado com sucesso!");
                        navigate("/");
                    }
                })
                .catch(error => {
                    setLoading(false); 
                    toast.error("Verifique todos os campos e tente novamente!");
                    console.error(error);
                });
        } else {
            toast.warn("Verifique todos os campos e tente novamente!");
        }
    }

    const handleFiltroApelido = (e: React.ChangeEvent<HTMLInputElement>) => {
        const novoNome = e.target.value.replace(/\s/g, '').slice(0, 15);
        setNome(novoNome);
    };

    return (
        <React.Fragment>
            <div className="page-register">
                <OutlinedCard>
                    <div className="logo-task">
                        <img src={logoSistemaAtividade} style={{
                            width: "250px",
                            height: "250px",
                            marginBottom: "10px"
                        }} />
                    </div>
                    <div className="form-register">
                        <Tooltip
                            title="Seu nome de usuário deve conter no máximo 15 caracteres e nenhum espaço em branco."
                            placement="bottom-start">
                            <TextField
                                label="Nome de Usuário:"
                                variant="outlined"
                                type="text"
                                value={nome}
                                onChange={handleFiltroApelido} />
                        </Tooltip>

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
                                type={showPassword ? 'text' : 'password'}
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                InputProps={{
                                    endAdornment: (
                                        <IconButton onClick={handleTogglePasswordVisibility}>
                                            {showPassword ? <Visibility /> : <VisibilityOff />}
                                        </IconButton>
                                    ),
                                }}
                            />
                        </Tooltip>

                        <Tooltip
                            title="A senha deve conter no mínimo 8 caracteres, um carácter maiúsculo e um carácter especial."
                            placement="bottom-start">
                            <TextField
                                label="Senha:"
                                variant="outlined"
                                type={showPassword ? 'text' : 'password'}
                                value={confirmPassword}
                                onChange={(e) => setConfirmPassword(e.target.value)}
                                InputProps={{
                                    endAdornment: (
                                        <IconButton onClick={handleTogglePasswordVisibility}>
                                            {showPassword ? <Visibility /> : <VisibilityOff />}
                                        </IconButton>
                                    ),
                                }}
                            />
                        </Tooltip>

                        <Button
                            variant="contained"
                            onClick={() => submitRequestRegisterUser()}>
                            Registrar-se
                        </Button>
                        <Button
                            variant="contained"
                            onClick={() => navigate("/")}>
                            Voltar
                        </Button>
                    </div>


                    <div className="extend-file-div">
                        <img src={logoExtendFile} alt="Logo eXtend File" width={150} />
                        <small id="extend-file">{new Date().getFullYear().toString()} Copyright © - Desenvolvido por eXtend File.</small>
                    </div>
                </OutlinedCard>
            </div>

            <LoadingProgress loading={loading}/>
        </React.Fragment>
    );
}