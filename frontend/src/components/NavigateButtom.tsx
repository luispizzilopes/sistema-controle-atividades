import * as React from 'react';
import BottomNavigation from '@mui/material/BottomNavigation';
import BottomNavigationAction from '@mui/material/BottomNavigationAction';
import FolderIcon from '@mui/icons-material/Folder';
import FavoriteIcon from '@mui/icons-material/Favorite';
import HomeIcon from '@mui/icons-material/Home';

interface NavigateButtomProps {
    abaSelecionada: string;
}

const NavigateButtom: React.FC<NavigateButtomProps> = ({ abaSelecionada }) => {
    return (
        <BottomNavigation
            sx={{
                width: 600,
                '@media (max-width:767px)': {
                    width: 300,
                },
            }}
                value={abaSelecionada}>
            <BottomNavigationAction
                label="Home"
                value="home"
                icon={<HomeIcon />}
            />
            <BottomNavigationAction
                label="Favorites"
                value="favorites"
                icon={<FavoriteIcon />}
            />
            <BottomNavigationAction
                label="Nearby"
                value="nearby"
                icon={<HomeIcon />}
            />
            <BottomNavigationAction label="Folder" value="folder" icon={<FolderIcon />} />
        </BottomNavigation>
    );
}

export default NavigateButtom;