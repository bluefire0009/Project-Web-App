import React from "react";

const ContactScreen: React.FC = () => {
    return (
        <div style={styles.container}>
            <div style={styles.card}>
                <h1 style={styles.title}>Contact Us</h1>
                <p style={styles.text}>
                    Email:{" "}
                    <a href="mailto:contact@example.com" style={styles.link}>
                        contact@example.com
                    </a>
                </p>
                <p style={styles.text}>
                    Phone:{" "}
                    <a href="tel:+1234567890" style={styles.link}>
                        +1 234 567 890
                    </a>
                </p>
            </div>
        </div>
    );
};

// Inline styles for the component
const styles: { [key: string]: React.CSSProperties } = {
    container: {
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "90vh",
        margin: 0,
    },
    card: {
        backgroundColor: "#ffffff",
        borderRadius: "8px",
        boxShadow: "0 4px 8px rgba(0, 0, 0, 0.1)",
        padding: "20px 30px",
        textAlign: "center",
        width: "300px",
    },
    title: {
        fontSize: "24px",
        color: "#333333",
        marginBottom: "20px",
    },
    text: {
        fontSize: "16px",
        color: "#555555",
        margin: "10px 0",
    },
    link: {
        color: "#007BFF",
        textDecoration: "none",
        fontWeight: "bold",
    },
};

export default ContactScreen;
