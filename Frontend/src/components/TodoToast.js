import Toast from "../../node_modules/react-bootstrap/esm/Toast";

const TodoToast = (props) => {
    return (
        <Toast show={props.show} onClose={props.onClose} delay={5000} autohide={true} bg={props.backgroundColour}>
          <Toast.Body>{props.text}</Toast.Body>
        </Toast>
    );
}

export default TodoToast;