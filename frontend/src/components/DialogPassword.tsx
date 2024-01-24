import React from 'react';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';

interface IDialogPassword {
    open: boolean;
    setOpen: (value: boolean) => void;
}

export default function DialogPassword({ open, setOpen }: IDialogPassword) {
    return (
        <React.Fragment>
            <Dialog
                open={open}
                onClose={() => setOpen(false)}
            >
                <DialogTitle>Solicitar Redefinição de Senha</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        Por favor, informe o seu e-mail registrado em nosso sistema. Em breve, você receberá um link na sua caixa de entrada para redefinir a sua senha.
                    </DialogContentText>
                    <TextField
                        autoFocus
                        required
                        margin="dense"
                        id="name"
                        name="email"
                        label="Email"
                        type="email"
                        fullWidth
                        variant="standard"
                    />
                </DialogContent>
                <DialogActions>
                    <Button variant='contained'>Enviar</Button>
                </DialogActions>
            </Dialog>
        </React.Fragment>
    );
}