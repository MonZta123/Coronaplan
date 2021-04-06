import React, { useState } from 'react';
import { Menubar } from 'primereact/menubar';

import { useHistory, useLocation } from 'react-router-dom';

const NavBar = () => {

    const history = useHistory();
    const location = useLocation();

    const getHandleCommand = (route) => () => history.push(route);

    const items = [
        { id: "/", label: 'Coronaplan', icon: 'pi pi-fw pi-question', command: getHandleCommand('/') },
        { id: "/swagger", label: 'Api', icon: 'pi pi-fw pi-pencil', command: getHandleCommand('/swagger') },
    ];

    const [activeItem, setActiveItem] = useState(items[items.findIndex(n => n.id === location.pathname)]);

    const handleTabChange = (evt) => {
        setActiveItem(items[items.indexOf(evt.value)]);
    };

    return <Menubar  model={items} activeItem={activeItem} onTabChange={handleTabChange}/>;
}

export default NavBar;