import axios from 'axios';
import React, { useEffect, useState } from 'react';

import Post from './Post';
import AuthController from '../../../core/api/AuthController';

const PostPage: React.FC = () => {
    const [data, setData] = useState({});
    
    useEffect(() => {
        const signin = async () => {
            var response = await AuthController.signIn({
                login: 'test',
                password: 'testPassword'
            });
            setData(response);
        };

        signin();
    }, [])

    console.log(data);
    return (
        <div>
            <Post></Post>
        </div>
    );
}

export default PostPage;