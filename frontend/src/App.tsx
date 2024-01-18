import RoutesApp from './Routes.tsx';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';

function App() {

  return (
    <>
      <ThemeProvider theme={darkTheme}>
        <CssBaseline />
        <RoutesApp />
      </ThemeProvider>
    </>
  )
}

const darkTheme = createTheme({
  palette: {
    mode: 'dark',
  },
});

export default App
