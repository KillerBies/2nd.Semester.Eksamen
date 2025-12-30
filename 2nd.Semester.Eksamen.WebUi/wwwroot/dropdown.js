window.getAnchorPosition = (el) => {
    if (!el) return null;
    const r = el.getBoundingClientRect();
    return {
        top: r.bottom,
        left: r.left
    };
};