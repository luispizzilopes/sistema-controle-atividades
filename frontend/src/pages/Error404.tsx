import './styles/Error404.css'
import ErrorIcon from '@mui/icons-material/Error';

export default function Error(){
    return(
        <div className="error404">
            <ErrorIcon sx={{
                height: "70px", 
                width: "70px"
            }}/>
            <h1>Error 404.</h1>
            <p>A página solicitada não existe no sistema.</p>
        </div>
    );
}