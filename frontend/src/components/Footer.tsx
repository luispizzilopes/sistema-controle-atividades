import './styles/footer.css';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';

export default function Footer() {

    return (
        <Box sx={{ width: '100%', position: "fixed", bottom: "2px", '@media (max-width:500px)': { display: 'none' } }}>
            <Typography sx={{ display: "flex", justifyContent: "center", fontSize: "12px" }}>
                {new Date().getFullYear().toString()} Copyright © - Desenvolvido por eXtend File.
            </Typography>
        </Box>
    );
}