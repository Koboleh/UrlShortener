import './App.css';
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import LoginPage from "./pages/LoginPage/LoginPage";

function App() {

  const router = createBrowserRouter([
    {
      path: "/login",
      element: <LoginPage />,
    }
  ]);
  return (
      <div className="App">
        <RouterProvider router={router}/>
      </div>
  );
}

export default App;
