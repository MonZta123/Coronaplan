import axios from 'axios';
import React, { useEffect, useState } from 'react';

import { ProgressSpinner } from 'primereact/progressspinner';
import { Card } from 'primereact/card';

const Plan = () => {

    const [plan, setPlan] = useState();

    const getPlan = () => {
        axios.get("api/plan").then((n) => setPlan(n.data.plan));
    }

    useEffect(() => {
        getPlan();
    }, []);

    const getData = (text) => <Card className="quote-card" title="Aktuellste Entscheidung"><div className="quote-text">"{text}"</div> </Card>;

    return (
        <div className="p-grid middle-thing">
            <div class="p-col-4">
                    {
                        plan && getData(plan)
                    }
                    {!plan && <ProgressSpinner />}
            </div>
        </div>);

}

export default Plan;