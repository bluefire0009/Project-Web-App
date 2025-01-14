import React, { createContext, useContext, useState, ReactNode } from 'react';

interface AdminPageStateContextProps {
    showCreateEventForm: boolean;
    setShowCreateEventForm: (show: boolean) => void;
    showEventList: boolean;
    setShowEventList: (show: boolean) => void;
}

const AdminPageStateContext = createContext<AdminPageStateContextProps | undefined>(undefined);

export const AdminPageStateProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [showCreateEventForm, setShowCreateEventForm] = useState(false);
    const [showEventList, setShowEventList] = useState(false);

    return (
        <AdminPageStateContext.Provider value={{ showCreateEventForm, setShowCreateEventForm, showEventList, setShowEventList }}>
            {children}
        </AdminPageStateContext.Provider>
    );
};

export const useAdminPageState = (): AdminPageStateContextProps => {
    const context = useContext(AdminPageStateContext);
    if (!context) {
        throw new Error('useAdminPageState must be used within an AdminPageStateProvider');
    }
    return context;
};